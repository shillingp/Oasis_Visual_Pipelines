using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
public sealed class BlockOperationGroupAttribute(BlockOperationType typeGroup, BlockOperationGrouping operationGroup)
    : Attribute
{
    public BlockOperationType TypeGroup { get; } = typeGroup;
    public BlockOperationGrouping OperationGroup { get; } = operationGroup;
}