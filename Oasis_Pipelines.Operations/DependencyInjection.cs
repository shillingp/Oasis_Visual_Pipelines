using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Operations;

namespace Oasis_Pipelines.Operations;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOperations()
        {
            IEnumerable<Type> blockOperationTypes = Assembly
                .GetAssembly(typeof(BlockOperation))!
                .GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false }
                    && typeof(BlockOperation).IsAssignableFrom(type)
                    && type != typeof(DefaultBlockOperation));

            foreach (Type blockOperationType in blockOperationTypes)
                services.AddTransient<BlockOperation>(serviceProvider =>
                    (BlockOperation)ActivatorUtilities.CreateInstance(serviceProvider, blockOperationType));

            services.AddKeyedTransient<BlockOperation>(BlockOperationGrouping.Other, (serviceProvider, _) =>
                ActivatorUtilities.CreateInstance<DefaultBlockOperation>(serviceProvider));

            return services;
        }
    }
}