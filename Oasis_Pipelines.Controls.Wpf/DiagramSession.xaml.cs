using System.Windows;
using System.Windows.Controls;
using Oasis_Pipelines.Controls.Wpf.Classes;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Controls.Wpf;

public partial class DiagramSession : UserControl
{
    private readonly DiagramSessionViewModel _viewModel;

    public ISessionContext? SessionContext
    {
        get => (ISessionContext?)GetValue(SessionContextProperty);
        set => SetValue(SessionContextProperty, value);
    }

    public static readonly DependencyProperty SessionContextProperty =
        DependencyProperty.Register(
            nameof(SessionContext),
            typeof(ISessionContext),
            typeof(DiagramSession),
            new PropertyMetadata(null, OnSessionContextChanged));

    public DiagramSession()
    {
        InitializeComponent();
        
        _viewModel = ControlServiceProvider.GetRequiredService<DiagramSessionViewModel>();
        RootGrid.DataContext = _viewModel;
    }
    
    private static void OnSessionContextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is not DiagramSession diagramSession
            || e.NewValue is not ISessionContext sessionContext)
            return;
        
        diagramSession._viewModel.SessionContext = sessionContext;
    }
}