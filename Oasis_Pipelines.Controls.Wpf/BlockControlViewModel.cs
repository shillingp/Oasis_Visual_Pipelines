using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Controls.Wpf;

public class BlockControlViewModel
{
    public Block? Block { get; set; }
    
    public bool IsExpanded { get; set; }
    public bool IsSelected { get; set; }
    
    public ICommand RemoveBlockCommand { get; }
    public ICommand ToggleBlockHeightCommand { get; }

    public BlockControlViewModel()
    {
        RemoveBlockCommand = new RelayCommand(RemoveBlock);
        ToggleBlockHeightCommand = new RelayCommand(ToggleBlockHeight);
    }

    private void RemoveBlock()
    {
        throw new NotImplementedException();
    }

    private void ToggleBlockHeight()
    {
        throw new NotImplementedException();
    }
}