using Android.Graphics.Drawables;
using FlexiMvvm.ValueConverters;
using Plugin.CurrentActivity;
using System;
using System.Globalization;

namespace vacation.droid.Converters
{
    public class NumberToImageValueConverter : ValueConverter<int, Drawable>
    {
        protected override ConversionResult<Drawable> Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            int image = 0;

            switch (value)
            {
                case 1:
                    image = Resource.Drawable.Icon_Request_Green;
                    break;
                case 2:
                    image = Resource.Drawable.Icon_Request_Plum;
                    break;
                case 3:
                    image = Resource.Drawable.Icon_Request_Gray;
                    break;
                case 4:
                    image = Resource.Drawable.Icon_Request_Dark;
                    break;
                case 5:
                    image = Resource.Drawable.Icon_Request_Blue;
                    break;
            }

            var drawable = CrossCurrentActivity.Current.AppContext.Resources.GetDrawable(image);
            return ConversionResult<Drawable>.SetValue(drawable);
        }
    }
}