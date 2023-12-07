using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Data;

namespace Oasis_Visual_Pipelines.Functions
{
    public static class HelperFunctions
    {
        internal static string ConvertNumberToLetters(int number)
        {
            if (number <= 26)
                return Convert.ToChar(number + 64).ToString();
            int div = number / 26;
            int mod = number % 26;
            if (mod == 0) { mod = 26; div--; }
            return ConvertNumberToLetters(div) + ConvertNumberToLetters(mod);
        }

        public static string[] ExtractColumnNamesFromTable(DataTable dataTable)
        {
            return dataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();
        }
    }
}
