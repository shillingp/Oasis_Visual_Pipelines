using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations;

public class AddNumbersOperation : BlockOperation
{
    /// <inheritdoc />
    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => inputOperations
            .Concat(additionalOperations)
            .Aggregate(0d, (total, item) => total + item.CalculateResult<double>()));
    }
}