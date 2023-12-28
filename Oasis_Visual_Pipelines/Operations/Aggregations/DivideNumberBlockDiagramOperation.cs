using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Operations
{
    internal class DivideNumberBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => int.MaxValue;

        public string OperationTitle => "Divide Numbers";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) =>
            {
                IEnumerable<BlockOperationResult> allOperations = additionalOperations
                    .Concat(inputOperations);

                return allOperations
                    .Skip(1)
                    .Aggregate(
                        (double)allOperations.First().Result(),
                        (total, item) => total + item.Result());
            });
        }
    }
}
