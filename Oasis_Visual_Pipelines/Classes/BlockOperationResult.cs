namespace Oasis_Visual_Pipelines.Classes
{
    public delegate dynamic ParamsFunc(params BlockOperationResult[] operationArguments);

    public record BlockOperationResult
    {
        public ParamsFunc Result;

        public BlockOperationResult(object data)
            : this(new BlockOperationResult(additionalInputs => data)) { }

        public BlockOperationResult(ParamsFunc executionFunction)
        {
            Result = executionFunction;
        }

        public static BlockOperationResult NullOperation = 
            new BlockOperationResult((additionalOperations) => null);
    }
}
