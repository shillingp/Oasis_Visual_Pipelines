using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Services.BlockManagement;

public interface IBlockManager
{
    /// <summary>
    /// A collection containing all active <see cref="Block"/> objects
    /// </summary>
    ICollection<Block> AllBlocks { get; set; }

    Block AddBlock(string blockTitle, BlockOperation blockOperation);

    bool RemoveBlock(Block block);
}