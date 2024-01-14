using Oasis_Visual_Pipelines.Classes;
using System.Globalization;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class FiltersDataTypeItemsSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Type tableColumnType)
                return Enumerable.Empty<string>();

            switch (tableColumnType)
            {
                case Type _ when tableColumnType == typeof(string):
                    return stringFilters;
                case Type _ when tableColumnType == typeof(int):
                case Type _ when tableColumnType == typeof(double):
                case Type _ when tableColumnType == typeof(decimal):
                case Type _ when tableColumnType == typeof(float):
                    return numericFilters;
                case Type _ when tableColumnType == typeof(DateTime):
                    return dateTimeFilters;
                default:
                    return Array.Empty<FilterFunctor>();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private readonly FilterFunctor[] stringFilters = [
            new FilterFunctor("Equals", "="),
            new FilterFunctor("Not equal", "<>"),
            new FilterFunctor("Contains", "LIKE '*___REPLACE___*'"),
            new FilterFunctor("Does not contain", "NOT LIKE '*___REPLACE___*'"),
            new FilterFunctor("Starts with", "LIKE '___REPLACE___*'"),
            new FilterFunctor("Ends with", "LIKE '*___REPLACE___'"),
            new FilterFunctor("Does not start with", "NOT LIKE '___REPLACE___*'"),
            new FilterFunctor("Does not end with", "NOT LIKE '*___REPLACE___'")
        ];

        private readonly FilterFunctor[] numericFilters = [
            new FilterFunctor("Equals", "="),
            new FilterFunctor("Not equal", "<>"),
            new FilterFunctor("Less than", "<"),
            new FilterFunctor("Less than or equal", "<="),
            new FilterFunctor("Greater than", ">"),
            new FilterFunctor("Greater than or equal", ">="),
        ];

        private readonly FilterFunctor[] dateTimeFilters = [
            new FilterFunctor("Equals", "= #__REPLACE__#"),
            new FilterFunctor("Not equal", "<> #__REPLACE__#"),
            new FilterFunctor("Less than", "< #__REPLACE__#"),
            new FilterFunctor("Less than or equal", "<= #__REPLACE__#"),
            new FilterFunctor("Greater than", "> #__REPLACE__#"),
            new FilterFunctor("Greater than or equal", ">= #__REPLACE__#"),
        ];
    }
}
