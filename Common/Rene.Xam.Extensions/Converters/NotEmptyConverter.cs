using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Rene.Xam.Extensions.Converters
{
    public class NotEmptyConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case string _:
                    return !string.IsNullOrEmpty(System.Convert.ToString(value));
                case IEnumerable<object> _:
                    var result = ((IEnumerable<object>)value).Any();
                    return result;

                case null:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
