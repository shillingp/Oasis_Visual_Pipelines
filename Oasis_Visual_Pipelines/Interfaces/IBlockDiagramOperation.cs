using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Classes.Messages;
using System.Diagnostics;

namespace Oasis_Visual_Pipelines.Interfaces
{
    public interface IBlockDiagramOperation
    {
        public int MaxInputs { get; }
        public int MaxOutputs => int.MaxValue;
        public string OperationTitle { get; }

        BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
    }
}
