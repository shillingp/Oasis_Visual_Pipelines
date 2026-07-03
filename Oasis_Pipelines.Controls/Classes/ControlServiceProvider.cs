using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Oasis_Pipelines.Controls.Classes;

internal static class ControlServiceProvider
{
    public static T GetRequiredService<T>()
        where T : notnull
    {
        if (Application.Current?.Resources["Services"] is not IServiceProvider serviceProvider)
            throw new InvalidOperationException(
                "The application service provider was not registered. " +
                "Set Application.Current.Resources[\"Services\"] = Host.Services during startup.");

        return serviceProvider.GetRequiredService<T>();
    }
}