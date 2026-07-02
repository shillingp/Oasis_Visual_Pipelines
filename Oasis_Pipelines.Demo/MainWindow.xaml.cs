using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Aggregations.Numbers;
using Oasis_Pipelines.Operations.Sources.Numbers;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ISessionManager SessionManager { get; }

    public MainWindow()
    {
        SessionManager = App.Host.Services.GetRequiredService<ISessionManager>();
        ISessionContext sessionContext = SessionManager.CreateContext();

        Block blockA = sessionContext.BlockManager.AddBlock("Input A", new NumberSourceOperation { NumberValue = 10d });
        Block blockB = sessionContext.BlockManager.AddBlock("Input B", new NumberSourceOperation { NumberValue = 3d });
        Block blockC = sessionContext.BlockManager.AddBlock("Middle A", new AddNumberOperation());
        Block blockD = sessionContext.BlockManager.AddBlock("Input C", new NumberSourceOperation { NumberValue = 5d });
        Block blockE = sessionContext.BlockManager.AddBlock("Output A", new AddNumberOperation());

        sessionContext.ConnectionManager.AddConnection(blockA, blockC);
        sessionContext.ConnectionManager.AddConnection(blockB, blockC);
        sessionContext.ConnectionManager.AddConnection(blockC, blockE);
        sessionContext.ConnectionManager.AddConnection(blockD, blockE);

        // object result = sessionContext.BlockCalculation
        //     .CalculateFlowPath(blockE)
        //     .CalculateResult();
        
        InitializeComponent();

        DataContext = this;
    }
}