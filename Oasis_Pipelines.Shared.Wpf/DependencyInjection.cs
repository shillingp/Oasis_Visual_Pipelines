using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Shared.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Services;

namespace Oasis_Pipelines.Shared.Wpf;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSharedWpf()
        {
            services.AddTransient<IDialogHostController, DialogHostController>();

            return services;
        }
    }
}