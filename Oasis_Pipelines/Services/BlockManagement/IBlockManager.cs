using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Services.BlockManagement;

/// <summary>
/// Interface for managing and interacting with blocks in the system.
/// </summary>
public interface IBlockManager
{
    /// <summary>
    /// A collection containing all active <see cref="Block"/> objects
    /// </summary>
    ICollection<Block> AllBlocks { get; }

    /// <summary>
    /// Adds a new block with the specified block operation to the block manager.
    /// </summary>
    /// <param name="blockOperation">
    /// The <see cref="BlockOperation"/> containing the operation logic and metadata for the block to be added.
    /// </param>
    /// <returns>
    /// A <see cref="Block"/> instance created using the provided operation.
    /// </returns>
    Block AddBlock(BlockOperation blockOperation);

    /// <summary>
    /// Adds a new block to the block manager with the specified title and block operation.
    /// </summary>
    /// <param name="blockTitle">
    /// The title of the block to be added.
    /// </param>
    /// <param name="blockOperation">
    /// The <see cref="BlockOperation"/> that defines the operation logic and metadata for the block.
    /// </param>
    /// <returns>
    /// A new <see cref="Block"/> instance created with the specified title and operation.
    /// </returns>
    Block AddBlock(string blockTitle, BlockOperation blockOperation);

    /// <summary>
    /// Removes the specified block from the block manager.
    /// </summary>
    /// <param name="block">
    /// The <see cref="Block"/> instance to be removed from the block manager's collection.
    /// </param>
    /// <returns>
    /// A boolean value indicating whether the removal operation was successful.
    /// </returns>
    bool RemoveBlock(Block block);
}