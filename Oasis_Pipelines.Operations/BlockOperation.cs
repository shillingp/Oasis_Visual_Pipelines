using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations;

public abstract class BlockOperation
{
    private BlockOperationResult _cachedResult = BlockOperationResult.NullOperation;
    private Guid _cachedCalculationId = Guid.Empty;

    public abstract string OperationTitle { get; }

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
        if (_cachedCalculationId == calculationId)
            return _cachedResult;

        _cachedCalculationId = calculationId;
        _cachedResult = ExecuteOperation(inputOperations);
        return _cachedResult;
    }
}