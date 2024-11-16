namespace Oasis_Visual_Pipelines.Classes
{
    public delegate dynamic? ParamsFunc(params BlockOperationResult[] operationArguments);

    public record BlockOperationResult
    {
        public readonly ParamsFunc Result;

        public BlockOperationResult(object data)
            : this(new BlockOperationResult(_ => data)) { }

        public BlockOperationResult(ParamsFunc executionFunction)
        {
            Result = (params BlockOperationResult[] operationArguments) =>
            {
                try
                {
                    return executionFunction(operationArguments);
                }
                catch (Exception exception)
                {
                    return new FailedOperation(exception);
                }
            };
        }

        public static readonly BlockOperationResult NullOperation =
            new BlockOperationResult(_ => null);
    }

    public record struct FailedOperation(Exception Error)
    {
        public FailedOperation(string errorMessage) : this(new Exception(errorMessage)) { }
    }
}
