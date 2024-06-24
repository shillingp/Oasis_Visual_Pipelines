using Oasis_Visual_Pipelines.Operations.Classes;

namespace Oasis_Visual_Pipelines.Operations.Interfaces
{
    public interface IBlockDiagramOperation
    {
        internal int MaxInputs { get; }
        internal int MaxOutputs => int.MaxValue;
        internal string OperationTitle { get; }
        
        internal abstract BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
    }
}
