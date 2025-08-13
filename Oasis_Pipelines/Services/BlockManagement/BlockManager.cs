using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Services.BlockManagement;

public sealed class BlockManager : IBlockManager
{
    /// <inheritdoc />
    public ICollection<Block> AllBlocks { get; set; } = [];

    /// <inheritdoc />
    public Block AddBlock(string blockTitle, BlockOperation blockOperation)
    {
        Block thing = new Block(blockTitle, blockOperation);

        AllBlocks.Add(thing);
        return thing;
    }

    /// <inheritdoc />
    public bool RemoveBlock(Block block)
    {
        return AllBlocks.Remove(block);
    }
}