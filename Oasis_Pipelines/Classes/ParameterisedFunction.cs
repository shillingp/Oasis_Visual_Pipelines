using OneOf;

namespace Oasis_Pipelines.Classes;

public delegate OneOf<ParameterisedFunction, object> ParameterisedFunction(
    params BlockOperationResult[] operationArguments);