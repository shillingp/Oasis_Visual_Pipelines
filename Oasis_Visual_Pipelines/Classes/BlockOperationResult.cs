namespace Oasis_Visual_Pipelines.Classes
{
    public delegate dynamic ParamsFunc(params BlockOperationResult[] operationArguments);

    public record BlockOperationResult
    {
        public ParamsFunc Result;

        public BlockOperationResult(ParamsFunc executionFunction)
        {
            Result = executionFunction;
        }
    }
}
