using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Interfaces;
using Oasis_Pipelines.Shared;
using Oasis_Pipelines.Shared.Interfaces;

namespace Oasis_Pipelines.Operations.Operations;

[BlockOperationGroup(BlockOperationType.None, BlockOperationGrouping.Other)]
public class DefaultBlockOperation : BlockOperation
{
    private readonly IEnumerable<BlockOperation> _blockOperations;
    private readonly IDialogHostController _dialogHostController;
    public override string OperationTitle => "Select Block";

    public ICommand ChooseBlockOperationTypeCommand { get; }

    public DefaultBlockOperation(
        IEnumerable<BlockOperation> blockOperations,
        IDialogHostController dialogHostController)
    {
        _blockOperations = blockOperations;
        _dialogHostController = dialogHostController;

        ChooseBlockOperationTypeCommand = new RelayCommand(ChooseBlockOperationType);
    }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => null);
    }

    private void ChooseBlockOperationType()
    {
        _dialogHostController.CreateAndShowDialog<IBlockPicker>();
    }
}