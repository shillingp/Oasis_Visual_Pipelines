using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Text.RegularExpressions;

namespace Oasis_Visual_Pipelines.Functions
{
    public static class HelperFunctions
    {
        public static string ConvertNumberToLetters(int number)
        {
            if (number <= 26)
                return Convert.ToChar(number + 64).ToString();
            int div = number / 26;
            int mod = number % 26;
            if (mod == 0) { mod = 26; div--; }
            return ConvertNumberToLetters(div) + ConvertNumberToLetters(mod);
        }

        public static bool IsValidRegex(string pattern)
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

        public static dynamic? ReturnBlockResult(Block block)
        {
            if (block is null ||
                !block.GetType().IsGenericType
                || block.GetType().GetGenericArguments()[0].GetInterface(nameof(IBlockDiagramOperation)) is null)
                return null;

            return ((dynamic)block).CalculateFlowPathResult()?.Result();
        }

        public static bool IsNumeric(object expression)
        {
            if (expression is null or DateTime)
                return false;

            if (expression is short or int or long or decimal or float or double)
                return true;

            return (expression is string && double.TryParse(expression as string, out double _))
                || double.TryParse(expression.ToString(), out double _);
        }

        public static double? ConvertNumeric(object number)
        {
            if (number is null or DateTime)
                return null;

            if (number is short or int or long or decimal or float or double)
                return (double)number;

            double parsedNumber = 0;
            if ((number is string && double.TryParse(number as string, out parsedNumber))
                || double.TryParse(number.ToString(), out double _))
                return parsedNumber;

            return null;
        }
    }
}
