using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Wpf.Converters;

public sealed class FiltersDataTypeItemsSourceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Type tableColumnType)
            return Enumerable.Empty<string>();

        switch (tableColumnType)
        {
            case not null when tableColumnType == typeof(string):
                return _stringFilters;
            case not null when tableColumnType == typeof(int):
            case not null when tableColumnType == typeof(double):
            case not null when tableColumnType == typeof(decimal):
            case not null when tableColumnType == typeof(float):
                return _numericFilters;
            case not null when tableColumnType == typeof(DateTime):
                return dateTimeFilters;
            default:
                return Array.Empty<FilterFunctor>();
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private readonly FilterFunctor[] _stringFilters =
    [
        new FilterFunctor("Equals", "="),
        new FilterFunctor("Not equal", "<>"),
        new FilterFunctor("Contains", "LIKE '*___REPLACE___*'"),
        new FilterFunctor("Does not contain", "NOT LIKE '*___REPLACE___*'"),
        new FilterFunctor("Starts with", "LIKE '___REPLACE___*'"),
        new FilterFunctor("Ends with", "LIKE '*___REPLACE___'"),
        new FilterFunctor("Does not start with", "NOT LIKE '___REPLACE___*'"),
        new FilterFunctor("Does not end with", "NOT LIKE '*___REPLACE___'")
    ];

    private readonly FilterFunctor[] _numericFilters =
    [
        new FilterFunctor("Equals", "="),
        new FilterFunctor("Not equal", "<>"),
        new FilterFunctor("Less than", "<"),
        new FilterFunctor("Less than or equal", "<="),
        new FilterFunctor("Greater than", ">"),
        new FilterFunctor("Greater than or equal", ">="),
    ];

    private readonly FilterFunctor[] dateTimeFilters =
    [
        new FilterFunctor("Equals", "= #___REPLACE___#"),
        new FilterFunctor("Not equal", "<> #___REPLACE___#"),
        new FilterFunctor("Less than", "< #___REPLACE___#"),
        new FilterFunctor("Less than or equal", "<= #___REPLACE___#"),
        new FilterFunctor("Greater than", "> #___REPLACE___#"),
        new FilterFunctor("Greater than or equal", ">= #___REPLACE___#"),
    ];
}