using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations;

public abstract class BlockOperation
{
    private BlockOperationResult cachedResult = BlockOperationResult.NullOperation;
    private Guid cachedCalculationId = Guid.Empty;

    /// <summary>
    /// Execute the operation using <paramref name="inputOperations"/> to generate an aggregate result
    /// </summary>
    /// <param name="calculationId"></param>
    /// <param name="inputOperations"></param>
    /// <returns></returns>
    protected abstract BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);

    internal BlockOperationResult ExecuteOperationCached(
        Guid calculationId,
        params BlockOperationResult[] inputOperations)
    {
        if (cachedCalculationId == calculationId)
            return cachedResult;

        cachedCalculationId = calculationId;
        cachedResult = ExecuteOperation(inputOperations);
        return cachedResult;
    }
}