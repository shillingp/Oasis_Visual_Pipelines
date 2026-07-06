using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

public interface IBlockDragController : IDragController
{
    public void SetBlock(Block block);
}

public interface IConnectionDragController : IDragController
{
    public void SetConnection(Connection? connection);
}