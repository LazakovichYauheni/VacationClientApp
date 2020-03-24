using Android.Views;
using FlexiMvvm.ValueConverters;
using System;
using System.Globalization;

namespace vacation.droid.Converters
{
    public class BoolToVisibilityValueConverter : ValueConverter<bool, ViewStates>
    {
        protected override ConversionResult<ViewStates> Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<ViewStates>.SetValue(value ? ViewStates.Visible : ViewStates.Invisible);
        }
    }
}