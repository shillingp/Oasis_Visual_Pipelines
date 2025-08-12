namespace Oasis_Pipelines.Classes;

public readonly record struct BlockOperationResult
{
    private readonly ParameterisedFunction result;

    public static readonly BlockOperationResult NullOperation =
        new BlockOperationResult(_ => null);

    public BlockOperationResult(ParameterisedFunction executionFunction)
    {
        result = RunSafely(executionFunction);
    }

    private ParameterisedFunction RunSafely(
        ParameterisedFunction executionFunction,
        params BlockOperationResult[] operationArguments)
    {
        try
        {
            return executionFunction(operationArguments)
                .Match(
                    parameterisedFunction => parameterisedFunction,
                    objectResult => _ => objectResult);
        }
        catch (Exception exception)
        {
            return _ => new FailedOperationException(exception);
        }
    }

    public object CalculateResult() => result().Value;

    public T CalculateResult<T>() => (T)result().AsT1;
}