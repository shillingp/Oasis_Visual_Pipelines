using System.Collections.ObjectModel;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Services.BlockManagement;

/// <summary>
/// Provides functionality for managing a collection of blocks within the system.
/// Implements the <see cref="IBlockManager"/> interface.
/// </summary>
public sealed class BlockManager : IBlockManager
{
    /// <inheritdoc />
    public ICollection<Block> AllBlocks { get; set; } = new ObservableCollection<Block>();

    /// <inheritdoc />
    public Block AddBlock(BlockOperation blockOperation) => AddBlock(blockOperation.OperationTitle, blockOperation);

    /// <inheritdoc />
    public Block AddBlock(string blockTitle, BlockOperation blockOperation)
    {
        Block newBlock = new Block(blockTitle, blockOperation);
        AllBlocks.Add(newBlock);
        return newBlock;
    }

    /// <inheritdoc />
    public bool RemoveBlock(Block block)
    {
        return AllBlocks.Remove(block);
    }
}