using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oasis_Pipelines.Controls;
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

            services.AddSingleton<ISessionManager, SessionManager>();
        })
        .Build();

    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Host.Start();
    }
}