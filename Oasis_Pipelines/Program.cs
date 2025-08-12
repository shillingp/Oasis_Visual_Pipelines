using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Services;

namespace Oasis_Pipelines;

public class Program
{
    private IHost Host => Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IBlockConnectionService, BlockConnectionService>();
            services.AddTransient<IBlockCalculationService, BlockCalculationService>();

            services.AddTransient<BlockOperation, NumberSourceOperation>();
            services.AddTransient<BlockOperation, AddNumbersOperation>();
        })
        .Build();

    static void Main(string[] args)
    {
        Block blockA = new Block("Input A", new NumberSourceOperation(10d));
        Block blockB = new Block("Input B", new NumberSourceOperation(3d));
        Block blockC = new Block("Middle A", new AddNumbersOperation());
        Block blockD = new Block("Input C", new NumberSourceOperation(5d));
        Block blockE = new Block("Output A", new AddNumbersOperation());

        BlockConnectionService blockConnectionService = new BlockConnectionService();
        blockConnectionService.AddConnection(blockA, blockC);
        blockConnectionService.AddConnection(blockB, blockC);
        blockConnectionService.AddConnection(blockC, blockE);
        blockConnectionService.AddConnection(blockD, blockE);

        BlockCalculationService blockCalculationService = new BlockCalculationService();
        object result = blockCalculationService
            .CalculateFlowPath(blockE)
            .CalculateResult();
    }
}