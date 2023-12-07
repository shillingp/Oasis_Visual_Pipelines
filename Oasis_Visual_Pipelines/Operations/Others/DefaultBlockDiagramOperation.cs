using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Controls;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using PropertyChanged;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.None, BlockOperationGroup.Other)]
    public class DefaultBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 0;
        public string OperationTitle => "Select Block";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations => null);
        }

        public ICommand ChooseBlockTypeCommand => new RelayCommand<BlockControl>(async (control) =>
        {
            Type blockOperationInterface = typeof(IBlockDiagramOperation);
            List<object> blockControlInstances = GenerateBlockControlInstancesForClassesDerivedFromType(blockOperationInterface);

            CollectionViewSource blockControlsViewSource = new CollectionViewSource();
            blockControlsViewSource.Source = blockControlInstances;
            blockControlsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("Block.Data", new BlockOperationGroupDataTypeConverter()));
            blockControlsViewSource.GroupDescriptions.Add(new PropertyGroupDescription("Block.Data", new BlockOperationGroupOperationTypeConverter()));

            Block chosenBlock = (Block)await DialogHostFunctions.CreateAndShowDialog(
                new BlockPickerDialog(),
                blockControlsViewSource,
                closeOnClickAway: true);
            if (chosenBlock is null) return;

            Point newPosition = control.Block.Position;
            newPosition.X += chosenBlock.CanvasElement.ActualWidth / 2;
            newPosition.Y += chosenBlock.CanvasElement.ActualHeight / 2;

            control.Block.BlockDiagram.AddBlock(
                newPosition,
                null,
                ((dynamic)chosenBlock).Data);

            control.Block.BlockDiagram.BlockDiagramItems.Remove(control.Block);
        });

        private static List<object> GenerateBlockControlInstancesForClassesDerivedFromType(Type blockOperationInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(assemblyType => blockOperationInterface.IsAssignableFrom(assemblyType))
                .Where(assemblyType => assemblyType.IsClass)
                .Where(assemblyType => assemblyType != typeof(DefaultBlockDiagramOperation))
                .Select(operationType =>
                {
                    Type genericBlockType = typeof(Block<>).MakeGenericType(operationType);
                    object operationTypeInstance = Activator.CreateInstance(operationType);
                    BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                    object genericBlockInstance = Activator.CreateInstance(
                        type: genericBlockType,
                        bindingAttr: flags,
                        binder: null,
                        args: [operationTypeInstance],
                        culture: CultureInfo.CurrentCulture);

                    ((Block)genericBlockInstance).Title = "Block";

                    return genericBlockInstance;
                })
                .Select(genericBlockOperationInstance =>
                    Activator.CreateInstance(typeof(BlockControl), genericBlockOperationInstance))
                .ToList();
        }
    }

    public class BlockOperationGroupDataTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BlockOperationGroupAttribute blockGroupAttribute = value.GetType()
                .GetCustomAttribute(typeof(BlockOperationGroupAttribute))
                as BlockOperationGroupAttribute;
            if (value is null) return null;

            return blockGroupAttribute.TypeGroup;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BlockOperationGroupOperationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BlockOperationGroupAttribute blockGroupAttribute = value.GetType()
                .GetCustomAttribute(typeof(BlockOperationGroupAttribute))
                as BlockOperationGroupAttribute;
            if (value is null) return null;

            return blockGroupAttribute.OperationGroup;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
