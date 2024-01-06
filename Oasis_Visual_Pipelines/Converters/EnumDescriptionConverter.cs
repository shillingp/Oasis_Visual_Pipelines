﻿using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumVal)
            {
                if (enumVal.GetType().GetField(enumVal.ToString()) is FieldInfo fi
                    && fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes 
                    && attributes.Any())
                    return attributes.First().Description;

                return value.ToString()!;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
