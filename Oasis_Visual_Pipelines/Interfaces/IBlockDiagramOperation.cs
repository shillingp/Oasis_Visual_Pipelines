using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Interfaces
{
    public interface IBlockDiagramOperation
    {
        BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
    }
}
