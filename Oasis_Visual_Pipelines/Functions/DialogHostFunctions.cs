using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace Oasis_Visual_Pipelines.Functions
{
    internal sealed class DialogHostFunctions
    {
        internal static async Task<object?> CreateAndShowDialog(object content, object dataContext, bool closeOnClickAway = true)
        {
            DialogHost newDialogHost = CreateDialog(content, dataContext, closeOnClickAway);

            return await newDialogHost.ShowDialog(content);
        }

        internal static DialogHost CreateDialog(object content, object dataContext, bool closeOnClickAway = true, Panel? rootPanel = null)
        {
            Window? activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            rootPanel ??= UIHelperFunctions.GetChildOfType<Panel>(activeWindow ?? Application.Current.MainWindow);

            if (rootPanel == null) throw new Exception("Unable to find panel to attach DialogHost to");

            string uniqueIdentifier = Guid.NewGuid().ToString();

            DialogHost newDialogHost = new DialogHost()
            {
                DataContext = dataContext,
                Identifier = uniqueIdentifier,
                DialogContent = content,
                CloseOnClickAway = closeOnClickAway,
            };

            if (rootPanel is Grid grid)
            {
                if (grid.RowDefinitions.Count > 0)
                    Grid.SetRowSpan(newDialogHost, grid.RowDefinitions.Count);
                if (grid.ColumnDefinitions.Count > 0)
                    Grid.SetColumnSpan(newDialogHost, grid.ColumnDefinitions.Count);
            }

            newDialogHost.DialogClosed += async (sender, eventArgs) =>
            {
                await Task.Delay(500);
                rootPanel.Children.Remove(newDialogHost);
            };

            rootPanel.Children.Add(newDialogHost);

            return newDialogHost;
        }
    }
}
