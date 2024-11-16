using Oasis_Visual_Pipelines.Controls;

namespace Oasis_Visual_Pipelines.Classes.Messages
{
    public sealed class BlockControlSelectionMessage(BlockControl? newSelection)
    {
        internal readonly BlockControl? NewSelection = newSelection;
    }
}
