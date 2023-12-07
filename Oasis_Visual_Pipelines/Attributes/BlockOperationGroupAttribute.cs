using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    internal sealed class BlockOperationGroupAttribute : Attribute
    {
        public BlockOperationGroupAttribute(BlockOperationType typeGroup, BlockOperationGroup operationGroup)
        {
            this.TypeGroup = typeGroup;
            this.OperationGroup = operationGroup;
        }

        public BlockOperationType TypeGroup { get; } = BlockOperationType.None;
        public BlockOperationGroup OperationGroup { get; } = BlockOperationGroup.Other;
    }
}
