using System.ComponentModel;

namespace Oasis_Visual_Pipelines.Operations.Enums
{
    public enum Authentication
    {
        None,
        Default,
        [Description("Username & Password")]
        UsernamePassword,
        Integrated,
        Interactive
    }
}
