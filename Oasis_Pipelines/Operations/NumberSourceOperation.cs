using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations;

public class NumberSourceOperation(double initialNumber) : BlockOperation
{
    /// <inheritdoc />
    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(_ => initialNumber);
    }
}