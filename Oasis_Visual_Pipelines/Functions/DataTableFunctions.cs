using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Functions
{
    internal static class DataTableFunctions
    {
        internal static string[] ExtractColumnNamesFromTable(DataTable dataTable)
        {
            return dataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();
        }

        internal static DataTable JoinDataTable(
            DataTable leftTable,
            DataTable rightTable,
            string leftJoinColumn,
            string rightJoinColumn)
        {
            DataTable resultTable = new DataTable();

            var joinTable = leftTable.AsEnumerable()
                .Join(
                    rightTable.AsEnumerable(),
                    leftRow => leftRow[leftJoinColumn],
                    rightRow => rightRow[rightJoinColumn],
                    (left, right) => new { left, right });
            //var joinTable = from t1 in dataTable1.AsEnumerable()
            //                join t2 in dataTable2.AsEnumerable()
            //                    on t1[joinField] equals t2[joinField]
            //                select new { t1, t2 };

            foreach (DataColumn column in leftTable.Columns)
                resultTable.Columns.Add(column.ColumnName, column.DataType);

            //resultTable.Columns.Remove(leftJoinColumn);

            foreach (DataColumn column in rightTable.Columns)
                resultTable.Columns.Add(column.ColumnName, column.DataType);

            resultTable.Columns.Remove(rightJoinColumn);

            foreach (var row in joinTable)
            {
                DataRow newRow = resultTable.NewRow();
                newRow.ItemArray = row.left.ItemArray.Union(row.right.ItemArray).ToArray();
                resultTable.Rows.Add(newRow);
            }

            return resultTable;
        }
    }
}
