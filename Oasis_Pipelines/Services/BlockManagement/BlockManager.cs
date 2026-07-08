using System.Collections.ObjectModel;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.BlockManagement;

/// <summary>
/// Provides functionality for managing a collection of blocks within the system.
/// Implements the <see cref="IBlockManager"/> interface.
/// </summary>
public sealed class BlockManager : IBlockManager
{
    private readonly IConnectionManager _connectionManager;

    public BlockManager(IConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }
    
    /// <inheritdoc />
    public ICollection<Block> AllBlocks { get; } = new ObservableCollection<Block>();

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
        Connection[] allConnections = [.. block.UpstreamConnections, .. block.DownstreamConnections];
        foreach (Connection connection in allConnections)
            _connectionManager.RemoveConnection(connection);
        
        return AllBlocks.Remove(block);
    }
}