using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Services.SessionManagement;
using Oasis_Pipelines.Shared.Wpf.Enums;
using Oasis_Pipelines.Shared.Wpf.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Services;

namespace Oasis_Pipelines.Controls.Wpf;

public class BlockControlViewModel
{
    private readonly ISessionManager _sessionManager;
    public Block? Block { get; set; }

    public bool IsExpanded { get; set; }
    public bool IsSelected { get; set; }

    public ICommand RemoveBlockCommand { get; }
    public ICommand ToggleBlockHeightCommand { get; }

    public BlockControlViewModel(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;

        RemoveBlockCommand = new RelayCommand(RemoveBlock);
        ToggleBlockHeightCommand = new RelayCommand(ToggleBlockHeight);
    }

    private void RemoveBlock()
    {
        if (Block is null) return;
        _sessionManager.CurrentSession?.BlockManager.RemoveBlock(Block);
    }

    private void ToggleBlockHeight()
    {
        throw new NotImplementedException();
    }
}