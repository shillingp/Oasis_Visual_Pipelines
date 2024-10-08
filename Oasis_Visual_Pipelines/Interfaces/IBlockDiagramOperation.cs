﻿using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Interfaces
{
    public interface IBlockDiagramOperation
    {
        public int MaxInputs { get; }
        public int MaxOutputs => int.MaxValue;
        public string OperationTitle { get; }

        public abstract BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations);
    }
}
