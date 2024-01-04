using ClosedXML.Excel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using Oasis_Visual_Pipelines.Operations;
using PropertyChanged;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for BlockDiagramControl.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class BlockDiagramControl : UserControl, IRecipient<BlockControlSelectionMessage>, IRecipient<BlockControlPropertyChangedMessage>
    {
        private static int numberOfBlocksCreated = 0;

        public ICollection<IBlockDiagramObject> BlockDiagramItems
        {
            get { return (ICollection<IBlockDiagramObject>)GetValue(BlockDiagramItemsProperty); }
            set { SetValue(BlockDiagramItemsProperty, value); }
        }

        public static readonly DependencyProperty BlockDiagramItemsProperty =
            DependencyProperty.Register(
                "BlockDiagramItems",
                typeof(ICollection<IBlockDiagramObject>),
                typeof(BlockDiagramControl),
                new PropertyMetadata(new ObservableSet<IBlockDiagramObject>()));

        public Type BlockDataType
        {
            get { return (Type)GetValue(BlockDataTypeProperty); }
            set { SetValue(BlockDataTypeProperty, value); }
        }

        public static readonly DependencyProperty BlockDataTypeProperty =
            DependencyProperty.Register(
                "BlockDataType",
                typeof(Type),
                typeof(BlockDiagramControl),
                new PropertyMetadata(typeof(DefaultBlockDiagramOperation)));

        public Block? SelectedBlock
        {
            get { return (Block)GetValue(SelectedBlockProperty); }
            set { SetValue(SelectedBlockProperty, value); }
        }

        public static readonly DependencyProperty SelectedBlockProperty =
            DependencyProperty.Register(
                "SelectedBlock",
                typeof(Block),
                typeof(BlockDiagramControl),
                new PropertyMetadata(null));

        public object SelectedBlockResult
        {
            get { return GetValue(SelectedBlockResultProperty); }
            set { SetValue(SelectedBlockResultProperty, value); }
        }

        public static readonly DependencyProperty SelectedBlockResultProperty =
            DependencyProperty.Register(
                "SelectedBlockResult",
                typeof(object),
                typeof(BlockDiagramControl),
                new PropertyMetadata(null));

        public BlockDiagramControl()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);

            InitializeComponent();
        }

        #region Commands
        public ICommand CopyBlockResultToClipboardCommand => new RelayCommand(() =>
        {
            dynamic? currentBlockResult = HelperFunctions.ReturnBlockResult(SelectedBlock!);

            if (currentBlockResult is null)
                return;

            if (currentBlockResult is not (DataTable or IEnumerable))
            {
                Clipboard.SetText(currentBlockResult.ToString());
                return;
            }

            if (currentBlockResult is DataTable resultTable)
            {
                string resultString = DataTableFunctions.ConvertDataTableToCSVString(resultTable);

                Clipboard.SetText(resultString, TextDataFormat.CommaSeparatedValue);
                return;
            }

            if (currentBlockResult is IEnumerable resultCollection)
            {
                StringBuilder resultString = new StringBuilder();

                foreach (object? item in resultCollection)
                    resultString.AppendLine(item.ToString());

                Clipboard.SetText(resultString.ToString(), TextDataFormat.Text);
                return;
            }

            Clipboard.SetDataObject(currentBlockResult);
        });

        public ICommand ExportBlockResultCommand => new RelayCommand(async () =>
        {
            dynamic? currentBlockResult = HelperFunctions.ReturnBlockResult(SelectedBlock!);

            if (currentBlockResult is null)
                return;

            if (currentBlockResult is not (DataTable or IEnumerable))
                await DialogHostFunctions.CreateAndShowDialog(
                    new TextBlock
                    {
                        Text = "Unable to export data.\nData must be a Table or a List",
                        Margin = new Thickness(5)
                    },
                    null,
                    true);

            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Oasis Export");

                if (currentBlockResult is DataTable resultantTable)
                    worksheet.FirstCell().InsertTable(resultantTable, true);
                else if (currentBlockResult is IEnumerable resultantCollection)
                    worksheet.FirstCell().InsertData(resultantCollection);

                string desktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory, Environment.SpecialFolderOption.None);
                workbook.SaveAs(Path.Combine(desktopLocation, "Oasis_Data_Export.xlsx"));
            }
        });
        #endregion

        #region Events
        public event RoutedEventHandler? CanvasLoaded;
        private void BlockDiagramCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Delay(100)
                .ContinueWith((_) =>
                    Dispatcher.Invoke(
                        RedrawAllBlocksAndConnections,
                        DispatcherPriority.Background));

            CanvasLoaded?.Invoke(sender, e);
        }

        private void BlockDiagramCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointRelativeToCanvas = e.GetPosition((Canvas)sender);

            HitTestResult elementUnderCursor = VisualTreeHelper.HitTest((Canvas)sender, pointRelativeToCanvas);

            if (elementUnderCursor is null || elementUnderCursor.VisualHit is not Canvas) return;

            WeakReferenceMessenger.Default.Send(new BlockControlSelectionMessage(null));

            Type contextType = typeof(Block<>).MakeGenericType(BlockDataType);
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            object? newBlockToAdd = Activator.CreateInstance(
                type: contextType,
                bindingAttr: flags,
                binder: null,
                args: [pointRelativeToCanvas, this],
                culture: CultureInfo.CurrentCulture);

            if (newBlockToAdd is not Block blockToAdd) return;

            blockToAdd.Title = "New Block";

            BlockDiagramItems.Add((IBlockDiagramObject)newBlockToAdd);
        }
        #endregion

        #region Methods
        public Block<T> AddBlock<T>(Point position, string? title = null, T? data = null)
            where T : class, new()
        {
            Block<T> newBlockToAdd = new Block<T>(position, this)
            {
                Data = data ?? new T()
            };

            if (title is not null)
                newBlockToAdd.Title = title;

            numberOfBlocksCreated++;
            if (string.IsNullOrEmpty(newBlockToAdd.Title))
                newBlockToAdd.Title = "Block " + HelperFunctions.ConvertNumberToLetters(numberOfBlocksCreated);

            BlockDiagramItems.Add(newBlockToAdd);

            return newBlockToAdd;
        }

        internal void RedrawAllBlocksAndConnections()
        {
            foreach (Block block in BlockDiagramItems.OfType<Block>())
                block.RedrawAnyConnections();
        }

        public void Receive(BlockControlSelectionMessage message)
        {
            SelectedBlock = message.NewSelection?.Block;
            SelectedBlockResult = HelperFunctions.ReturnBlockResult(SelectedBlock!)!;
        }

        public void Receive(BlockControlPropertyChangedMessage message)
        {
            SelectedBlockResult = HelperFunctions.ReturnBlockResult(SelectedBlock!)!;
        }
        #endregion
    }
}
