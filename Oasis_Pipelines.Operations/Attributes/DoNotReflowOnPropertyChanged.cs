namespace Oasis_Pipelines.Operations.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class DoNotReflowOnPropertyChangedAttribute : Attribute { }