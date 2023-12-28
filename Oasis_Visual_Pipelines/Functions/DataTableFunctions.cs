﻿using System.Data;

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

            IEnumerable<(DataRow Left, DataRow Right)> joinTable = leftTable.AsEnumerable()
                .Join(rightTable.AsEnumerable(),
                    leftRow => leftRow[leftJoinColumn],
                    rightRow => rightRow[rightJoinColumn],
                    (left, right) => (Left: left, Right: right));

            foreach (DataColumn column in leftTable.Columns)
                resultTable.Columns.Add(column.ColumnName, column.DataType);

            foreach (DataColumn column in rightTable.Columns)
                resultTable.Columns.Add("new-" + column.ColumnName, column.DataType);

            foreach ((DataRow Left, DataRow Right) in joinTable)
            {
                DataRow newRow = resultTable.NewRow();
                newRow.ItemArray = Left.ItemArray.Concat(Right.ItemArray).ToArray();
                resultTable.Rows.Add(newRow);
            }

            resultTable.Columns.Remove("new-" + rightJoinColumn);

            return resultTable;
        }
    }
}