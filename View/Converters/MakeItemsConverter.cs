using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using ExposureMachine.Common;
using System.Collections.ObjectModel;

namespace ExposureMachine.View.Converters
{
    class MakeItemsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<int> dict;
            Buttons tag;
            var res = new ObservableCollection<(Buttons,int)>();
            try
            {
                dict = (ObservableCollection<int>)values[0];
                tag = (Buttons)values[1]!;
                foreach (var item in dict)
                {
                    res.Add((tag, item));
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
