using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oasis_Pipelines.Controls.Wpf;
using Oasis_Pipelines.Dialogs.Wpf;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;
using Oasis_Pipelines.Shared.Wpf;

namespace Oasis_Pipelines.Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddPipelines();
            services.AddControls();
            services.AddDialogs();
            services.AddOperations();
            services.AddSharedWpf();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>();
        })
        .Build();

    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host.Start();

        Resources["Services"] = _host.Services;

        MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs e)
    {
        _host.Dispose();

        base.OnExit(e);
    }
}