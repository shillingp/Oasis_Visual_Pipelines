using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services;

public interface IBlockCalculationService
{
    public BlockOperationResult CalculateFlowPath(Block block, Guid calculationId);
}