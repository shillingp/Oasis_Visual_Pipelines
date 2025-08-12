using System.Diagnostics.CodeAnalysis;

namespace Oasis_Pipelines.Model;

public class Connection
{
    public int UpstreamHashCache;

    [field: MaybeNull]
    public string ConnectionTitle
    {
        get => field ?? LeftBlock.BlockTitle + "->" + RightBlock.BlockTitle;
        init;
    }
    public Block LeftBlock { get; set; }
    public Block RightBlock { get; set; }

    public Connection(Block leftBlock, Block rightBlock)
    {
        LeftBlock = leftBlock;
        RightBlock = rightBlock;
    }

    public Connection(string connectionTitle, Block leftBlock, Block rightBlock)
        : this(leftBlock, rightBlock)
    {
        ConnectionTitle = connectionTitle;
    }
}