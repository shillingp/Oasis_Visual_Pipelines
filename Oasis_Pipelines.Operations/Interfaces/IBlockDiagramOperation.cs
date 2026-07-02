using Oasis_Pipelines.Operations.Classes;

namespace Oasis_Pipelines.Operations.Interfaces;

public interface IBlockDiagramOperation
{
    public int MaxInputs { get; }
    public int MaxOutputs => int.MaxValue;
    public string OperationTitle { get; }

    public abstract BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
}