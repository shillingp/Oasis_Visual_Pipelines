using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.BlockCalculation;

public interface IBlockCalculation
{
    public BlockOperationResult CalculateFlowPath(Block block, Guid? calculationId = null);
}