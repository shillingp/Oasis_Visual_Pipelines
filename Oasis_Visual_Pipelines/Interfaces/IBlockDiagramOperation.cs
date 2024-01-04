using Oasis_Visual_Pipelines.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
