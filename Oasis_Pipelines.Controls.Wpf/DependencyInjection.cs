using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Controls.Wpf.Services;
using Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

namespace Oasis_Pipelines.Controls.Wpf;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddControls()
        {
            services.AddSingleton<IConnectorVisualRegistry, ConnectorVisualRegistry>();

            services.AddTransient<BlockDragController>();
            services.AddTransient<IConnectionDragController, ConnectorDragController>();
            
            services.AddTransient<BlockControlViewModel>();
            services.AddTransient<ConnectorNodeViewModel>();
            services.AddTransient<DiagramSessionManagerViewModel>();
            services.AddTransient<DiagramSessionViewModel>();

            return services;
        }
    }
}