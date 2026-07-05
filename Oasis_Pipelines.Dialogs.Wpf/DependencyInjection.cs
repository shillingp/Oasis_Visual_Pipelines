using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Operations.Interfaces;

namespace Oasis_Pipelines.Dialogs.Wpf;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDialogs()
        {
            services.AddTransient<IBlockPicker, BlockPicker>();
            services.AddTransient<BlockPickerViewModel>();
            
            return services;
        }
    }
}