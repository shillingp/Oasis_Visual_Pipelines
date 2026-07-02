using OneOf;

namespace Oasis_Pipelines.Operations.Classes;

public delegate OneOf<ParameterisedFunction, object> ParameterisedFunction(
    params BlockOperationResult[] operationArguments);