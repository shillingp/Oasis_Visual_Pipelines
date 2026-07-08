using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Services.SessionManagement;
using Oasis_Pipelines.Shared.Wpf.Extensions;
using PropertyChanged;

namespace Oasis_Pipelines.Controls.Wpf;

[AddINotifyPropertyChangedInterface]
public class DiagramSessionViewModel
{
    private readonly BlockOperation _defaultBlockOperation;

    public ISessionContext? SessionContext { get; set; }
    public ICommand AddBlockCommand { get; }

    public DiagramSessionViewModel(
        [FromKeyedServices(BlockOperationGrouping.Other)]
        BlockOperation defaultBlockOperation)
    {
        _defaultBlockOperation = defaultBlockOperation;

        AddBlockCommand = new RelayCommand<MouseButtonEventArgs>(AddBlock);
    }

    private void AddBlock(MouseButtonEventArgs? e)
    {
        Block? newBlock = SessionContext?.BlockManager.AddBlock(_defaultBlockOperation);
        newBlock?.Position = Mouse.GetPosition((e.Source as FrameworkElement).FindAncestor<Canvas>()).ToPointF();
    }
}