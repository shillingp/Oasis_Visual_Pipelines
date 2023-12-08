using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Interfaces
{
    public interface IBlockDiagramOperation
    {
        public int MaxInputs { get; }
        public string OperationTitle { get; }

        BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
    }
}
