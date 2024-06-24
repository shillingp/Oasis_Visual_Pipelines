using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class BlockOperationGroupAttribute(BlockOperationType typeGroup, BlockOperationGrouping operationGroup) : Attribute
    {
        public BlockOperationType TypeGroup { get; } = typeGroup;
        public BlockOperationGrouping OperationGroup { get; } = operationGroup;
    }
}
