using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Enums
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
