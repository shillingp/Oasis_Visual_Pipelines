using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services;

public class BlockCalculationService : IBlockCalculationService
{
    /// <inheritdoc />
    public BlockOperationResult CalculateFlowPath(Block block, Guid calculationId = default)
    {
        if (calculationId == Guid.Empty)
            calculationId = Guid.NewGuid();

        BlockOperationResult[] upstreamOperationResults = block.UpstreamConnections
            .Select(connection => connection.LeftBlock)
            .Select(innerBlock => CalculateFlowPath(innerBlock, calculationId))
            .ToArray();

        return block.Operation.ExecuteOperationCached(calculationId, upstreamOperationResults);
    }
}