using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Oasis_Pipelines.Operations;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBlockOperations()
        {
            IEnumerable<Type> blockOperationTypes = Assembly
                .GetAssembly(typeof(BlockOperation))!
                .GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false }
                    && typeof(BlockOperation).IsAssignableFrom(type));

            foreach (Type blockOperationType in blockOperationTypes)
                services.AddTransient<BlockOperation>(serviceProvider =>
                    (BlockOperation)ActivatorUtilities.CreateInstance(serviceProvider, blockOperationType));

            return services;
        }
    }
}