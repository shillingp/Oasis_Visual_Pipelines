using System.Drawing;

namespace Oasis_Pipelines.Shared.Wpf.Extensions;

public static class PointExtensions
{
    extension(System.Windows.Point point)
    {
        public PointF ToPointF() => new PointF((float)point.X, (float)point.Y);
    }
    
    extension(PointF point)
    {
        public System.Windows.Point ToPoint() => new System.Windows.Point(point.X, point.Y);
        public SizeF ToSizeF() => new SizeF(point.X, point.Y);
    }
}