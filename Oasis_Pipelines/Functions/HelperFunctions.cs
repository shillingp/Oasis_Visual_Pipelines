namespace Oasis_Pipelines.Functions;

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

    public static bool IsNumeric(object expression)
    {
        if (expression is null or DateTime)
            return false;

        if (expression is short or int or long or decimal or float or double)
            return true;

        return (expression is string text && double.TryParse(text, out double _))
            || double.TryParse(expression.ToString(), out double _);
    }
}