using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UserManage.Common;

namespace UserManage.Converter
{
    internal class StateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value; // State가 정수인 경우를 가정합니다.

            if (state == Codes.UserState.USE)
            {
                return "사용";
            }
            else if (state == Codes.UserState.PAUSE)
            {
                return "중지";
            }
            else if(state == Codes.UserState.EXPIRE)
            {
                return "만료";
            }
            
            return state.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
