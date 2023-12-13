﻿namespace Oasis_Visual_Pipelines.Classes
{
    public delegate dynamic ParamsFunc(params BlockOperationResult[] operationArguments);

    public record BlockOperationResult
    {
        public ParamsFunc Result;

        public BlockOperationResult(object data)
            : this(new BlockOperationResult(additionalInputs => data)) { }

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

        public static BlockOperationResult NullOperation = 
            new BlockOperationResult((additionalOperations) => null);
    }

    public record struct FailedOperation(Exception error);
}
