using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Aggregations.Numbers;
using Oasis_Pipelines.Operations.Sources.Numbers;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Diagrams;

public class DiagramSessionViewModel
{
    public ISessionManager SessionManager { get; }

    public DiagramSessionViewModel(ISessionManager sessionManager)
    {
        SessionManager = sessionManager;

        ISessionContext sessionContext = SessionManager.CreateContext();
        
        Block blockA = sessionContext.BlockManager.AddBlock("Input A", new NumberSourceOperation(10d));
        Block blockB = sessionContext.BlockManager.AddBlock("Input B", new NumberSourceOperation(3d));
        Block blockC = sessionContext.BlockManager.AddBlock("Middle A", new AddNumberOperation());
        Block blockD = sessionContext.BlockManager.AddBlock("Input C", new NumberSourceOperation(5d));
        Block blockE = sessionContext.BlockManager.AddBlock("Output A", new AddNumberOperation());

        sessionContext.ConnectionManager.AddConnection(blockA, blockC);
        sessionContext.ConnectionManager.AddConnection(blockB, blockC);
        sessionContext.ConnectionManager.AddConnection(blockC, blockE);
        sessionContext.ConnectionManager.AddConnection(blockD, blockE);

        object result = sessionContext.BlockCalculation
            .CalculateFlowPath(blockE)
            .CalculateResult();
    }
}