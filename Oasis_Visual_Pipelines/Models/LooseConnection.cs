using Oasis_Visual_Pipelines.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Oasis_Visual_Pipelines.Models
{
    public class LooseConnection : IBlockDiagramObject<Line>, IBlockDiagramObject, IBlockDiagramConnection
    {
        public static Brush ConnectionColor { get; } = Brushes.LightGray;

        public Point Position { get; set; }

        public Point Start { get; }

        private Point end;
        public Point End
        {
            get => end;
            internal set
            {
                end = value;

                UpdateCanvasElementVector();
            }
        }

        public Line CanvasElement { get; }

        public LooseConnection()
        {
            CanvasElement = CreateDefaultCanvasElement();
        }

        public LooseConnection(double x, double y) : this(new Point(x, y)) { }

        public LooseConnection(Point initialLocation) : this()
        {
            Start = initialLocation;
            End = initialLocation;
        }

        public Line CreateDefaultCanvasElement()
        {
            return new Line
            {
                Stroke = ConnectionColor,
                StrokeThickness = 5,
            };
        }

        public void UpdateCanvasElementVector()
        {
            Line lineElement = CanvasElement;

            lineElement.X1 = Start.X;
            lineElement.Y1 = Start.Y;
            lineElement.X2 = End.X;
            lineElement.Y2 = End.Y;
        }
    }
}
