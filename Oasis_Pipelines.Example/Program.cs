using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oasis_Pipelines.Diagrams;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Operations.Aggregations.Numbers;
using Oasis_Pipelines.Operations.Sources.Numbers;
using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Example;

public class Program
{
    private static IHost Host => Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddTransient<IBlockManager, BlockManager>();

            services.AddTransient<IBlockCalculation, BlockCalculation>();

            services.AddTransient<BlockOperation, NumberSourceOperation>();
            services.AddTransient<BlockOperation, AddNumberOperation>();

            services.AddTransient<ISessionManager, SessionManager>();
            services.AddTransient<ISessionContext, SessionContext>();
            services.AddTransient<ISessionContextFactory, SessionContextFactory>();

            services.AddTransient<DiagramSession>();
            services.AddTransient<DiagramSessionViewModel>();
        })
        .Build();

    [STAThread]
    static void Main(string[] args)
    {
        Host.Services
            .GetRequiredService<DiagramSession>()
            .ShowDialog();
    }
}