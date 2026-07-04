using Microsoft.Extensions.DependencyInjection;

namespace Oasis_Pipelines.Controls.Wpf;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddControls()
        {
            services.AddTransient<BlockControlViewModel>();
            services.AddTransient<DiagramSessionManagerViewModel>();
            services.AddTransient<DiagramSessionViewModel>();

            return services;
        }
    }
}