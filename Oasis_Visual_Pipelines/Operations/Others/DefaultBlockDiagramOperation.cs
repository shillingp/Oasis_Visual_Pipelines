using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Controls;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations.Others
{
    [BlockOperationGroup(BlockOperationType.None, BlockOperationGroup.Other)]
    public class DefaultBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override int MaxOutputs => 0;
        public override string OperationTitle => "Select Block";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations => null);
        }

        public static ICommand ChooseBlockTypeCommand => new RelayCommand<BlockControl>(async (control) =>
        {
            Type blockOperationInterface = typeof(BaseBlockDiagramOperation);

            object?[] blockOperationInstances = GenerateBlockControlInstancesForClassesDerivedFromTypeAsync(blockOperationInterface);

            Block? chosenBlock = (Block?)await DialogHostFunctions.CreateAndShowDialog(
                new BlockPickerDialog(),
                blockOperationInstances,
                closeOnClickAway: true);
            if (chosenBlock is null) return;

            Point newPosition = control!.Block.Position;
            newPosition.X += chosenBlock.CanvasElement.ActualWidth / 2;
            newPosition.Y += chosenBlock.CanvasElement.ActualHeight / 2;

            control.Block.BlockDiagram!.AddBlock(
                newPosition,
                null,
                ((dynamic)chosenBlock).Data);

            control.Block.BlockDiagram.BlockDiagramItems.Remove(control.Block);
        });

        private static object?[] GenerateBlockControlInstancesForClassesDerivedFromTypeAsync(Type blockOperationInterface)
        {
            if (blockOperationInterface is null) throw new ArgumentNullException(nameof(blockOperationInterface));

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(assemblyType => blockOperationInterface.IsAssignableFrom(assemblyType))
                .Where(assemblyType => assemblyType.IsClass)
                .Where(assemblyType => assemblyType != typeof(DefaultBlockDiagramOperation))
                .Where(assemblyType => assemblyType != typeof(BaseBlockDiagramOperation))
                .Select(operationType =>
                {
                    Type genericBlockType = typeof(Block<>).MakeGenericType(operationType);
                    object? operationTypeInstance = Activator.CreateInstance(operationType);
                    if (operationTypeInstance is null) return null;

                    BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                    object? genericBlockInstance = Activator.CreateInstance(
                        type: genericBlockType,
                        bindingAttr: flags,
                        binder: null,
                        args: [operationTypeInstance],
                        culture: CultureInfo.CurrentCulture);
                    if (genericBlockInstance is null) return null;

                    ((Block)genericBlockInstance).Title = "Block";

                    return genericBlockInstance;
                })
                .Select(genericBlockOperationInstance =>
                    Activator.CreateInstance(typeof(BlockControl), genericBlockOperationInstance))
                .ToArray();
        }
    }
}
