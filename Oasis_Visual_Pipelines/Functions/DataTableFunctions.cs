using ExcelDataReader;
using Oasis_Visual_Pipelines.Classes;
using System.Data;
using System.IO;
using System.Text;

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

        internal static DataColumn[] ExtractColumnsFromTable(DataTable dataTable)
        {
            return dataTable.Columns
                .Cast<DataColumn>()
                .ToArray();
        }

        internal static DataTable FilterDataTable(DataTable tableObject, ObservableSet<DataTableFilter> selectedFilters, string sqlJoinStatement = "AND")
        {
            if (tableObject is null) throw new ArgumentNullException(nameof(tableObject));
            if (selectedFilters is null) throw new ArgumentNullException(nameof(selectedFilters));

            DataTable resultTable = tableObject.Copy();
            resultTable.DefaultView.RowFilter = string.Join(
                " " + sqlJoinStatement + " ",
                selectedFilters.Select(filter => filter.ToString()));

            return resultTable;
        }

        internal static object ConcatDataTables(DataTable leftDataTable, DataTable rightDataTable)
        {
            DataTable resultTable = leftDataTable.Copy();

            string[] leftHandColumns = leftDataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            foreach (DataRow dataRow in rightDataTable.Rows.Cast<DataRow>())
                resultTable.Rows.Add(leftHandColumns.Select(column => dataRow[column]));

            return resultTable;
        }

        internal static DataTable JoinDataTables(
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

        internal static string ConvertDataTableToCSVString(DataTable resultTable)
        {
            StringBuilder resultString = new StringBuilder();

            IEnumerable<string> columnNames = resultTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName);
            resultString.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in resultTable.Rows)
            {
                IEnumerable<string?> fields = row.ItemArray.Select(field => field!.ToString());
                resultString.AppendLine(string.Join(",", fields));
            }

            return resultString.ToString();
        }

        internal static DataTable ImportExcelToDataTable(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                return new DataTable();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });

                return dataset.Tables[0];
            }
        }
    }
}
