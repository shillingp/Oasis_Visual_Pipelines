using System.Windows;
using System.Windows.Controls;
using Oasis_Pipelines.Controls.Wpf.Classes;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Controls.Wpf;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class DiagramSessionManager : UserControl
{
    private readonly DiagramSessionManagerViewModel _viewModel;

    public DiagramSessionManager()
    {
        InitializeComponent();
        
        _viewModel = ControlServiceProvider.GetRequiredService<DiagramSessionManagerViewModel>();
        RootGrid.DataContext = _viewModel;
    }
}