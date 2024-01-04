using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Classes.Messages
{
    public class BlockControlSelectedResultMessage(object Result)
    {
        public readonly object Result = Result;
    }
}
