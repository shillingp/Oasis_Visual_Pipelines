using Oasis_Visual_Pipelines.Controls;

namespace Oasis_Visual_Pipelines.Classes.Messages
{
    public class BlockControlSelectionMessage(BlockControl NewSelection)
    {
        internal readonly BlockControl NewSelection = NewSelection;
    }
}
