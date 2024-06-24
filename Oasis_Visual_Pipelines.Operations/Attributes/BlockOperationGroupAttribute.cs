using Oasis_Visual_Pipelines.Operations.Enums;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class BlockOperationGroupAttribute(BlockOperationType typeGroup, BlockOperationGroup operationGroup) : Attribute
    {
        public BlockOperationType TypeGroup { get; } = typeGroup;
        public BlockOperationGroup OperationGroup { get; } = operationGroup;
    }
}
