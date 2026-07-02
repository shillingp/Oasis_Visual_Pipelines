using System.Drawing;

namespace Oasis_Pipelines.Operations.Interfaces;

public interface IBlockDiagramObject
{
    public Point Position { get; }
}

public interface IBlockDiagramObject<T>
{
    public T CanvasElement { get; }

    T CreateDefaultCanvasElement();
}