using Oasis_Visual_Pipelines.Controls;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Oasis_Visual_Pipelines.Models
{
    public class Connection : IBlockDiagramObject<Path>, IBlockDiagramObject, IBlockDiagramConnection
    {
        public static Brush ConnectionColor { get; } = Brushes.OrangeRed;

        public Point Position { get; set; } = new Point(0, 0);

        public Point Start { get; protected set; }
        public Point Finish { get; protected set; }

        public Block? LeftBlock { get; }
        public Block? RightBlock { get; }

        public Path CanvasElement { get; }

        public Connection()
        {
            CanvasElement = CreateDefaultCanvasElement();
        }

        public Connection(Block blockOne, Block blockTwo) : this()
        {
            LeftBlock = blockOne;
            RightBlock = blockTwo;

            UpdateCanvasElementVector();
        }

        public Path CreateDefaultCanvasElement()
        {
            return new Path
            {
                Stroke = ConnectionColor,
                StrokeThickness = 5,
            };
        }

        public void UpdateCanvasElementVector()
        {

            ConnectorNodeControl? leftNode = LeftBlock?.GetConnectionNode(this);
            ConnectorNodeControl? rightNode = RightBlock?.GetConnectionNode(this);

            if (leftNode is null || rightNode is null) return;

            Point start = UIHelperFunctions.GetFrameworkElementCenter(leftNode);
            Point finish = UIHelperFunctions.GetFrameworkElementCenter(rightNode);

            start.X += ConnectorNodeControl.ConnectorNodeSize / 2;
            finish.X -= ConnectorNodeControl.ConnectorNodeSize / 2;

            CanvasElement.Data = LeftBlock!.BlockDiagram!.ConnectionStyle switch
            {
                Enums.ConnectionStyle.Bezier => GenerateBezier(start, finish),
                Enums.ConnectionStyle.RightAngle => GenerateRightAngle(start, finish),
                Enums.ConnectionStyle.Straight => GenerateStraight(start, finish),
                _ => throw new NotImplementedException(),
            };
        }

        private static Geometry GenerateBezier(Point start, Point finish)
        {
            double horizontalOffset = Math.Abs((finish - start).X);
            int bezierExtentFromBlock = (int)horizontalOffset / 2;

            Point[] points = [
                new Point(start.X + bezierExtentFromBlock, start.Y),
                new Point(finish.X - bezierExtentFromBlock, finish.Y),
                finish
            ];

            return new PathGeometry()
            {
                Figures =
                [
                    new PathFigure(start, [ new PolyBezierSegment(points, true) ], false)
                ]
            };
        }

        private static Geometry GenerateRightAngle(Point start, Point finish)
        {
            Point[] points = [
                new Point(start.X + Math.Abs(finish.X - start.X) / 2, start.Y),
                new Point(start.X + Math.Abs(finish.X - start.X) / 2, finish.Y),
                finish
            ];

            return new PathGeometry
            {
                Figures =
                [
                    new PathFigure(start, [ new PolyLineSegment(points, true) ], false)
                ]
            };
        }

        private static Geometry GenerateStraight(Point start, Point finish)
        {
            return new PathGeometry
            {
                Figures =
                [
                    new PathFigure(start, [ new PolyLineSegment([finish], true) ], false)
                ]
            };
        }
    }
}
