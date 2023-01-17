using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Assignment4.Converters;

public class InvertBool : MarkupExtension, IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool b
            ? !b
            : value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool b
            ? !b
            : value;

    public override object ProvideValue(IServiceProvider serviceProvider) => this;

}
