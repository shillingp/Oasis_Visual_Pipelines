using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    internal sealed class BlockOperationGroupAttribute(BlockOperationType typeGroup, BlockOperationGroup operationGroup) : Attribute
    {
        public BlockOperationType TypeGroup { get; } = typeGroup;
        public BlockOperationGroup OperationGroup { get; } = operationGroup;
    }
}
