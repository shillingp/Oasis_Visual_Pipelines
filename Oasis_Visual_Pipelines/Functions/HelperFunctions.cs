using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Text.RegularExpressions;

namespace Oasis_Visual_Pipelines.Functions
{
    internal static class HelperFunctions
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

        public static dynamic? ReturnBlockResult(Block block)
        {
            if (block is null ||
                !block.GetType().IsGenericType
                || block.GetType().GetGenericArguments()[0].GetInterface(nameof(IBlockDiagramOperation)) is null)
                return null;

            return ((dynamic)block).CalculateFlowPathResult()?.Result();
        }
    }
}
