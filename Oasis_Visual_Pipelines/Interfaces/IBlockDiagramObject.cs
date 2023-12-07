using System.Windows;

namespace Oasis_Visual_Pipelines.Interfaces
{
    public interface IBlockDiagramObject
    {
        public Point Position { get; }
    }

    public interface IBlockDiagramObject<T>
    {
        public T CanvasElement { get; }

        T CreateDefaultCanvasElement();
    }
}
