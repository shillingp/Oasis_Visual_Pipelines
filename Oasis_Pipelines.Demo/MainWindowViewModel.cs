using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Operations.Aggregations.Numbers;
using Oasis_Pipelines.Operations.Operations.Sources.Numbers;
using Oasis_Pipelines.Services.SessionManagement;
using PropertyChanged;

namespace Oasis_Pipelines.Demo;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel
{
    public MainWindowViewModel(ISessionManager sessionManager)
    {
        ISessionContext sessionContext = sessionManager.CreateContext();

        Block blockA = sessionContext.BlockManager.AddBlock("Input A", new NumberSourceOperation { NumberValue = 10d });
        Block blockB = sessionContext.BlockManager.AddBlock("Input B", new NumberSourceOperation { NumberValue = 3d });
        Block blockC = sessionContext.BlockManager.AddBlock("Middle A", new AddNumberOperation());
        Block blockD = sessionContext.BlockManager.AddBlock("Input C", new NumberSourceOperation { NumberValue = 5d });
        Block blockE = sessionContext.BlockManager.AddBlock("Output A", new AddNumberOperation());

        sessionContext.ConnectionManager.AddConnection(blockA, blockC);
        sessionContext.ConnectionManager.AddConnection(blockB, blockC);
        sessionContext.ConnectionManager.AddConnection(blockC, blockE);
        sessionContext.ConnectionManager.AddConnection(blockD, blockE);
    }
}