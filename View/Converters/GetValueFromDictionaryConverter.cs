using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using ExposureMachine.Common;


namespace ExposureMachine.View.Converters
{
    class GetValueFromDictionaryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<Buttons, int> dict;
            Buttons key;
            int res = 0;
            try
            {
                dict = (Dictionary<Buttons, int>)values[0];
                key = (Buttons)values[1]!;
                res = dict[key]-1;
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
            //return new object[] { new Dictionary<Buttons, int>() };
        }
    }
}
