namespace Oasis_Pipelines.Operations.Classes;

public sealed class FailedOperationException : Exception
{
    public FailedOperationException(Exception innerException) : base("Failed operation", innerException)
    {
    }

    public FailedOperationException(string message) : base(message)
    {
    }
}