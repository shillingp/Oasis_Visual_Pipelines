﻿using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using Oasis_Visual_Pipelines.Operations;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for BlockDiagramControl.xaml
    /// </summary>
    public partial class BlockDiagramControl : UserControl, IRecipient<BlockControlSelectionMessage>
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

        public Block SelectedBlock
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

        public BlockDiagramControl()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);

            InitializeComponent();
        }

        public event RoutedEventHandler CanvasLoaded;
        private void BlockDiagramCanvas_Loaded(object sender, RoutedEventArgs e)
        {
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
            object newBlockToAdd = Activator.CreateInstance(
                type: contextType,
                bindingAttr: flags,
                binder: null,
                args: [pointRelativeToCanvas, this],
                culture: CultureInfo.CurrentCulture);

            ((Block)newBlockToAdd).Title = "New Block";

            BlockDiagramItems.Add((IBlockDiagramObject)newBlockToAdd);
        }

        public Block<T> AddBlock<T>(Point position, string title = null, T data = null)
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
        }
    }
}