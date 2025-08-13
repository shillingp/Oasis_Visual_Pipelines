using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.BlockCalculation;

public sealed class BlockCalculation : IBlockCalculation
{
    /// <inheritdoc />
    public BlockOperationResult CalculateFlowPath(Block block, Guid? calculationId = null)
    {
        calculationId ??= Guid.CreateVersion7();

        BlockOperationResult[] upstreamOperationResults = block.UpstreamConnections
            .Select(connection => connection.LeftBlock)
            .Select(innerBlock => CalculateFlowPath(innerBlock, calculationId))
            .ToArray();

        return block.Operation.ExecuteOperationCached((Guid)calculationId, upstreamOperationResults);
    }
}