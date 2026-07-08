using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Oasis_Pipelines.Controls.Wpf.Classes;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Shared.Wpf.Extensions;

namespace Oasis_Pipelines.Controls.Wpf.Converters;

[ValueConversion(typeof(object[]), typeof(PathGeometry))]
public class GeneratePathGeometryConverter : IMultiValueConverter
{
    private readonly IConnectorVisualRegistry _connectorVisualRegistry;

    public GeometryType GeometryPathMethod { get; set; } = GeometryType.Bezier;

    public enum GeometryType
    {
        Bezier,
        RightAngle,
        Straight
    }

    public GeneratePathGeometryConverter()
    {
        _connectorVisualRegistry = ControlServiceProvider.GetRequiredService<IConnectorVisualRegistry>();
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [Connection connector, ..])
            return new PathGeometry();

        _connectorVisualRegistry.TryGetConnectorNode(
            connector, ConnectionSide.Right, out ConnectorNode? leftConnectorNode);
        _connectorVisualRegistry.TryGetConnectorNode(
            connector, ConnectionSide.Left, out ConnectorNode? rightConnectorNode);
        if (leftConnectorNode is null || rightConnectorNode is null)
            return new PathGeometry();

        Point startPoint = leftConnectorNode.GetFrameworkElementCenter().ToPoint();
        Point endPoint = rightConnectorNode.GetFrameworkElementCenter().ToPoint();

        return GeometryCalculationFunction(GeometryPathMethod)(startPoint, endPoint);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static Func<Point, Point, Geometry> GeometryCalculationFunction(GeometryType geometryType) =>
        geometryType switch
        {
            GeometryType.Bezier => GenerateBezier,
            GeometryType.RightAngle => GenerateRightAngle,
            GeometryType.Straight => GenerateStraight,
            _ => throw new ArgumentOutOfRangeException(nameof(geometryType), geometryType, null)
        };

    private static Geometry GenerateBezier(Point start, Point finish)
    {
        double horizontalOffset = Math.Abs((finish - start).X);
        int bezierExtentFromBlock = (int)horizontalOffset / 2;

        Point[] points =
        [
            start with { X = start.X + bezierExtentFromBlock },
            finish with { X = finish.X - bezierExtentFromBlock },
            finish
        ];

        return new PathGeometry
        {
            Figures =
            [
                new PathFigure
                {
                    StartPoint = start,
                    Segments = [new PolyBezierSegment(points, true)]
                }
            ]
        };
    }

    private static Geometry GenerateRightAngle(Point start, Point finish)
    {
        Point[] points =
        [
            start with { X = start.X + Math.Abs(finish.X - start.X) / 2 },
            finish with { X = start.X + Math.Abs(finish.X - start.X) / 2 },
            finish
        ];

        return new PathGeometry
        {
            Figures =
            [
                new PathFigure
                {
                    StartPoint = start,
                    Segments = [new PolyLineSegment(points, true)]
                }
            ]
        };
    }

    private static Geometry GenerateStraight(Point start, Point finish)
    {
        return new PathGeometry
        {
            Figures =
            [
                new PathFigure
                {
                    StartPoint = start,
                    Segments = [new PolyLineSegment([finish], true)]
                }
            ]
        };
    }
}