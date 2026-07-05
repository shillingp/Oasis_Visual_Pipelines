using System.Windows.Controls;
using Oasis_Pipelines.Operations.Interfaces;

namespace Oasis_Pipelines.Dialogs.Wpf;

public partial class BlockPicker : UserControl, IBlockPicker
{
    public BlockPicker(BlockPickerViewModel viewModel)
    {
        InitializeComponent();
    }
}