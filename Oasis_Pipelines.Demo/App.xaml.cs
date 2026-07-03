using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oasis_Pipelines.Controls;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static readonly IHost Host = Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddTransient<IBlockManager, BlockManager>();

            services.AddTransient<IBlockCalculation, BlockCalculation>();

            services.AddSingleton<ISessionManager, SessionManager>();
            services.AddSingleton<ISessionContextFactory, SessionContextFactory>();

            services.AddBlockOperations();
            services.AddControls();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>();
        })
        .Build();

    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Host.Start();

        Resources["Services"] = Host.Services;
        
        MainWindow mainWindow = Host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs e)
    {
        Host.Dispose();

        base.OnExit(e);
    }
}