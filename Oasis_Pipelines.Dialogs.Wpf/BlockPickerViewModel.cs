using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Dialogs.Wpf;

public class BlockPickerViewModel
{
    public IEnumerable<BlockOperation> Operations { get; }

    public BlockPickerViewModel(IEnumerable<BlockOperation> operations)
    {
        Operations = operations;
    }
}