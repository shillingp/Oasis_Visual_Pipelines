using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace Oasis_Pipelines.Shared.Wpf.Extensions;

public static class UiExtensions
{
    extension(DependencyObject? current)
    {
        public T? FindAncestor<T>() where T : DependencyObject
        {
            if (current is null) return null;

            do
            {
                if (current is T matchedType)
                    return matchedType;

                current = VisualTreeHelper.GetParent(current);
            } while (current != null);

            return null;
        }

        public IEnumerable<T> FindVisualChildren<T>() where T : DependencyObject
        {
            if (current is null)
                yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);
                if (child is T matchedType)
                    yield return matchedType;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public T? GetChildOfType<T>()
            where T : DependencyObject
        {
            if (current == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);

                T? result = (child as T) ?? child.GetChildOfType<T>();
                if (result != null) return result;
            }

            return null;
        }
    }

    extension(FrameworkElement element)
    {
        public Point GetFrameworkElementCenter()
        {
            Point position = element.TranslatePoint(
                new Point(0, 0), element.FindAncestor<Canvas>());

            position.X += element.ActualWidth / 2;
            position.Y += element.ActualHeight / 2;

            return position;
        }

        public PointF ClipFrameworkElementPointWithinCanvas(PointF position)
        {
            Canvas? canvasAncestor = FindAncestor<Canvas>(element);
            if (canvasAncestor is null)
                return position;

            return new PointF(
                (float)Math.Min(Math.Max(0, position.X), canvasAncestor.ActualWidth - element.ActualWidth),
                (float)Math.Min(Math.Max(0, position.Y), canvasAncestor.ActualHeight - element.ActualHeight));
        }
    }
}