using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Classes;

namespace Oasis_Pipelines.Services.BlockCalculation;

public interface IBlockCalculation
{
    public BlockOperationResult CalculateFlowPath(Block block, Guid? calculationId = null);
}