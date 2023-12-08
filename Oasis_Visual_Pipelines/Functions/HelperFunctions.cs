using System.Data;
using System.Text.RegularExpressions;

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

        internal static string[] ExtractColumnNamesFromTable(DataTable dataTable)
        {
            return dataTable.Columns
                .Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();
        }

        internal static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
