using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Shared.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Extensions;

namespace Oasis_Pipelines.Shared.Wpf.Services;

public class DialogHostController : IDialogHostController
{
    private readonly IServiceProvider _serviceProvider;

    public DialogHostController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<object?> CreateAndShowDialog<TDialogViewModel>()
        where TDialogViewModel : IDialog
    {
        try
        {
            IDialog dialog = _serviceProvider.GetRequiredService<TDialogViewModel>();

            if (dialog is not FrameworkElement dialogView)
                throw new InvalidOperationException(
                    $"Dialog type '{typeof(IDialog).FullName}' must resolve to a WPF FrameworkElement.");

            return await CreateDialog(dialogView).ShowDialog(dialogView);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static DialogHost CreateDialog<TView>(
        TView content,
        bool closeOnClickAway = true,
        Panel? rootPanel = null)
    {
        Window? activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        rootPanel ??= (activeWindow ?? Application.Current.MainWindow).GetChildOfType<Panel>();

        if (rootPanel == null) throw new Exception("Unable to find panel to attach DialogHost to");

        string uniqueIdentifier = Guid.NewGuid().ToString();

        DialogHost newDialogHost = new DialogHost
        {
            DataContext = (content as FrameworkElement)?.DataContext,
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

        newDialogHost.DialogClosed += async (_, _) =>
        {
            await Task.Delay(500);
            rootPanel.Children.Remove(newDialogHost);
        };

        rootPanel.Children.Add(newDialogHost);

        return newDialogHost;
    }
}