using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Assignment4.Converters;

public class IsNotNull : MarkupExtension, IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is not null;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();

    public override object ProvideValue(IServiceProvider serviceProvider) => this;

}
