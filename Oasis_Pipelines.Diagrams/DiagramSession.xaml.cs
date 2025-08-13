using System.Windows;

namespace Oasis_Pipelines.Diagrams;

public partial class DiagramSession : Window
{
    public DiagramSession(DiagramSessionViewModel sessionManagerViewModel)
    {
        DataContext = sessionManagerViewModel;

        InitializeComponent();
    }
}