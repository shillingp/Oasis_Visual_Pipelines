using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Oasis_Visual_Pipelines.Functions
{
    public static class UIHelperFunctions
    {
        public static T? FindAncestor<T>(DependencyObject? current)
            where T : DependencyObject
        {
            if (current is null) return null;

            do
            {
                if (current is T matchedType)
                    return matchedType;

                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);

            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject? depObj) where T : DependencyObject
        {
            if (depObj is null)
                yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is not null and T matchedType)
                    yield return matchedType;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public static T? GetChildOfType<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                T? result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static Point GetFrameworkElementCenter(FrameworkElement element)
        {
            Point position = element.TranslatePoint(
                new Point(0, 0),
                FindAncestor<Canvas>(element));

            position.X += element.ActualWidth / 2;
            position.Y += element.ActualHeight / 2;

            return position;
        }

        public static Point ClipFrameworkElementPointWithinCanvas(FrameworkElement sourceElement, Point position)
        {
            Canvas? canvasAncestor = FindAncestor<Canvas>(sourceElement);
            if (canvasAncestor is null)
                return position;

            return new Point(
                Math.Min(Math.Max(0, position.X), canvasAncestor.ActualWidth - sourceElement.ActualWidth),
                Math.Min(Math.Max(0, position.Y), canvasAncestor.ActualHeight - sourceElement.ActualHeight));
        }
    }
}
