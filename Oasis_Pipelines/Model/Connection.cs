using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Oasis_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Pipelines.Model;

[AddINotifyPropertyChangedInterface]
public class Connection : IConnection
{
    [field: MaybeNull]
    public string ConnectionTitle
    {
        get => field ?? LeftBlock.BlockTitle + "->" + RightBlock.BlockTitle;
        init;
    }

    public Block LeftBlock { get; set; }
    public Block RightBlock { get; set; }

    /// <inheritdoc />
    public PointF Position { get; set; }

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

    public void Disconnect()
    {
        LeftBlock.DownstreamConnections.Remove(this);
        RightBlock.UpstreamConnections.Remove(this);
        // LeftBlock = null;
        // RightBlock = null;
    }
}