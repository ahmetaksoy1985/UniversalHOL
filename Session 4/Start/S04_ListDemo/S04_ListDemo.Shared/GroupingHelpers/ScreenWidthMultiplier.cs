using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace S04_ListDemo.GroupingHelpers
{
    public class ScreenWidthMultiplier : IValueConverter
    {
        public ScreenWidthMultiplier()
        {
            Percentage = 0.9;//default 90% of screenwidth. 
            MinWidth = 0;
        }

        public double MinWidth { get; set; }

        public double Percentage { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double width = Window.Current.CoreWindow.Bounds.Width * Percentage *
                   System.Convert.ToDouble(parameter);

            if (width >= MinWidth)
                return width;
            else
                return MinWidth;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
