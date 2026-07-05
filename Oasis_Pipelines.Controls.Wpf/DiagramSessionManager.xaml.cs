using System.Windows.Controls;
using Oasis_Pipelines.Controls.Wpf.Classes;

namespace Oasis_Pipelines.Controls.Wpf;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class DiagramSessionManager : UserControl
{
    public DiagramSessionManager()
    {
        InitializeComponent();

        DiagramSessionManagerViewModel viewModel =
            ControlServiceProvider.GetRequiredService<DiagramSessionManagerViewModel>();
        RootGrid.DataContext = viewModel;
    }
}