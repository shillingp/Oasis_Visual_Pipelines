using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPipelines()
        {
            services.AddTransient<IConnectionManager, ConnectionManager>();
            services.AddTransient<IBlockManager, BlockManager>();
            services.AddTransient<IBlockCalculation, BlockCalculation>();
            services.AddSingleton<ISessionManager, SessionManager>();
            services.AddSingleton<ISessionContextFactory, SessionContextFactory>();

            return services;
        }
    }
}