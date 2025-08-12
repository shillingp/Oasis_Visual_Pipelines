namespace Oasis_Pipelines.Classes;

public sealed class FailedOperationException(Exception innerException) 
    : Exception("Failed operation", innerException);