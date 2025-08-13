namespace Oasis_Pipelines.Classes;

public readonly record struct BlockOperationResult
{
    private readonly ParameterisedFunction _result;

    public static readonly BlockOperationResult NullOperation =
        new BlockOperationResult(_ => null);

    public BlockOperationResult(object data)
    {
        _result = _ => data;
    }

    public BlockOperationResult(ParameterisedFunction executionFunction)
    {
        _result = RunSafely(executionFunction);
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

    public object CalculateResult(params BlockOperationResult[] optionalInputs) => _result(optionalInputs).Value;

    public T CalculateResult<T>(params BlockOperationResult[] optionalInputs) => (T)_result(optionalInputs).AsT1;
}