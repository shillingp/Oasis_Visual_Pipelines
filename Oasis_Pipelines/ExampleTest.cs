using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Aggregations.Numbers;
using Oasis_Pipelines.Operations.Sources.Numbers;
using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines;

public class ExampleTest
{
    public ExampleTest(
        IBlockManager blockManager,
        IConnectionManager connectionManager,
        IBlockCalculation blockCalculation)
    {
        Block blockA = blockManager.AddBlock("Input A", new NumberSourceOperation(10d));
        Block blockB = blockManager.AddBlock("Input B", new NumberSourceOperation(3d));
        Block blockC = blockManager.AddBlock("Middle A", new AddNumberOperation());
        Block blockD = blockManager.AddBlock("Input C", new NumberSourceOperation(5d));
        Block blockE = blockManager.AddBlock("Output A", new AddNumberOperation());

        connectionManager.AddConnection(blockA, blockC);
        connectionManager.AddConnection(blockB, blockC);
        connectionManager.AddConnection(blockC, blockE);
        connectionManager.AddConnection(blockD, blockE);

        object result = blockCalculation
            .CalculateFlowPath(blockE)
            .CalculateResult();
    }
}