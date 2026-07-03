using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Services.SessionManagement;
using PropertyChanged;

namespace Oasis_Pipelines.Controls;

[AddINotifyPropertyChangedInterface]
public class DiagramSessionViewModel
{
    private readonly IEnumerable<BlockOperation> _blockOperations;

    public ISessionContext? SessionContext { get; set; }
    public ICommand AddBlockCommand { get; }

    public DiagramSessionViewModel(IEnumerable<BlockOperation> blockOperations)
    {
        _blockOperations = blockOperations;

        AddBlockCommand = new RelayCommand<MouseButtonEventArgs>(AddBlock);
    }

    private void AddBlock(MouseButtonEventArgs? e)
    {
        BlockOperation defaultBlockOperation = _blockOperations.First(block =>
            block.GetType().GetCustomAttribute<BlockOperationGroupAttribute>() is
            {
                TypeGroup: BlockOperationType.None,
                OperationGroup: BlockOperationGrouping.Other
            });
        SessionContext?.BlockManager.AddBlock(defaultBlockOperation);
    }
}