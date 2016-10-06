namespace YKMaze.Views.Converters
{
    using System;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;
    using YKMaze.Models;

    public class EnumToCurrentImageConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (MapStatus)value;
            string uri;
            switch (type)
            {
                case MapStatus.Stop_None_Left_None_Right_None: uri = "Resources/img/Stop_None_Left_None_Right_None.png"; break;
                case MapStatus.Stop_None_Left_None_Right_Pre0: uri = "Resources/img/Stop_None_Left_None_Right_Pre0.png"; break;
                case MapStatus.Stop_None_Left_None_Right_Pre1: uri = "Resources/img/Stop_None_Left_None_Right_Pre1.png"; break;
                case MapStatus.Stop_None_Left_None_Right_Pre2: uri = "Resources/img/Stop_None_Left_None_Right_Pre2.png"; break;
                case MapStatus.Stop_None_Left_None_Right_Pre0_Pre2: uri = "Resources/img/Stop_None_Left_None_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Right_None: uri = "Resources/img/Stop_None_Left_Pre0_Right_None.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Right_Pre0: uri = "Resources/img/Stop_None_Left_Pre0_Right_Pre0.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Right_Pre1: uri = "Resources/img/Stop_None_Left_Pre0_Right_Pre1.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Right_Pre2: uri = "Resources/img/Stop_None_Left_Pre0_Right_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Right_Pre0_Pre2: uri = "Resources/img/Stop_None_Left_Pre0_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre1_Right_None: uri = "Resources/img/Stop_None_Left_Pre1_Right_None.png"; break;
                case MapStatus.Stop_None_Left_Pre1_Right_Pre0: uri = "Resources/img/Stop_None_Left_Pre1_Right_Pre0.png"; break;
                case MapStatus.Stop_None_Left_Pre1_Right_Pre1: uri = "Resources/img/Stop_None_Left_Pre1_Right_Pre1.png"; break;
                case MapStatus.Stop_None_Left_Pre1_Right_Pre2: uri = "Resources/img/Stop_None_Left_Pre1_Right_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre1_Right_Pre0_Pre2: uri = "Resources/img/Stop_None_Left_Pre1_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre2_Right_None: uri = "Resources/img/Stop_None_Left_Pre2_Right_None.png"; break;
                case MapStatus.Stop_None_Left_Pre2_Right_Pre0: uri = "Resources/img/Stop_None_Left_Pre2_Right_Pre0.png"; break;
                case MapStatus.Stop_None_Left_Pre2_Right_Pre1: uri = "Resources/img/Stop_None_Left_Pre2_Right_Pre1.png"; break;
                case MapStatus.Stop_None_Left_Pre2_Right_Pre2: uri = "Resources/img/Stop_None_Left_Pre2_Right_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre2_Right_Pre0_Pre2: uri = "Resources/img/Stop_None_Left_Pre2_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Pre2_Right_None: uri = "Resources/img/Stop_None_Left_Pre0_Pre2_Right_None.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Pre2_Right_Pre0: uri = "Resources/img/Stop_None_Left_Pre0_Pre2_Right_Pre0.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Pre2_Right_Pre1: uri = "Resources/img/Stop_None_Left_Pre0_Pre2_Right_Pre1.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Pre2_Right_Pre2: uri = "Resources/img/Stop_None_Left_Pre0_Pre2_Right_Pre2.png"; break;
                case MapStatus.Stop_None_Left_Pre0_Pre2_Right_Pre0_Pre2: uri = "Resources/img/Stop_None_Left_Pre0_Pre2_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_Pre0_Left_None_Right_None: uri = "Resources/img/Stop_Pre0_Left_None_Right_None.png"; break;
                case MapStatus.Stop_Pre0_Left_None_Right_Pre0: uri = "Resources/img/Stop_Pre0_Left_None_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre0_Left_Pre0_Right_None: uri = "Resources/img/Stop_Pre0_Left_Pre0_Right_None.png"; break;
                case MapStatus.Stop_Pre0_Left_Pre0_Right_Pre0: uri = "Resources/img/Stop_Pre0_Left_Pre0_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre1_Left_None_Right_None: uri = "Resources/img/Stop_Pre1_Left_None_Right_None.png"; break;
                case MapStatus.Stop_Pre1_Left_None_Right_Pre0: uri = "Resources/img/Stop_Pre1_Left_None_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre1_Left_None_Right_Pre1: uri = "Resources/img/Stop_Pre1_Left_None_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre0_Right_None: uri = "Resources/img/Stop_Pre1_Left_Pre0_Right_None.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre0_Right_Pre0: uri = "Resources/img/Stop_Pre1_Left_Pre0_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre0_Right_Pre1: uri = "Resources/img/Stop_Pre1_Left_Pre0_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre1_Right_None: uri = "Resources/img/Stop_Pre1_Left_Pre1_Right_None.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre1_Right_Pre0: uri = "Resources/img/Stop_Pre1_Left_Pre1_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre1_Left_Pre1_Right_Pre1: uri = "Resources/img/Stop_Pre1_Left_Pre1_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_None_Right_None: uri = "Resources/img/Stop_Pre2_Left_None_Right_None.png"; break;
                case MapStatus.Stop_Pre2_Left_None_Right_Pre0: uri = "Resources/img/Stop_Pre2_Left_None_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre2_Left_None_Right_Pre1: uri = "Resources/img/Stop_Pre2_Left_None_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_None_Right_Pre2: uri = "Resources/img/Stop_Pre2_Left_None_Right_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_None_Right_Pre0_Pre2: uri = "Resources/img/Stop_Pre2_Left_None_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Right_None: uri = "Resources/img/Stop_Pre2_Left_Pre0_Right_None.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Right_Pre0: uri = "Resources/img/Stop_Pre2_Left_Pre0_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Right_Pre1: uri = "Resources/img/Stop_Pre2_Left_Pre0_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Right_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre0_Right_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Right_Pre0_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre0_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre1_Right_None: uri = "Resources/img/Stop_Pre2_Left_Pre1_Right_None.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre1_Right_Pre0: uri = "Resources/img/Stop_Pre2_Left_Pre1_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre1_Right_Pre1: uri = "Resources/img/Stop_Pre2_Left_Pre1_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre1_Right_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre1_Right_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre1_Right_Pre0_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre1_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre2_Right_None: uri = "Resources/img/Stop_Pre2_Left_Pre2_Right_None.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre2_Right_Pre0: uri = "Resources/img/Stop_Pre2_Left_Pre2_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre2_Right_Pre1: uri = "Resources/img/Stop_Pre2_Left_Pre2_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre2_Right_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre2_Right_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre2_Right_Pre0_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre2_Right_Pre0_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_None: uri = "Resources/img/Stop_Pre2_Left_Pre0_Pre2_Right_None.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre0: uri = "Resources/img/Stop_Pre2_Left_Pre0_Pre2_Right_Pre0.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre1: uri = "Resources/img/Stop_Pre2_Left_Pre0_Pre2_Right_Pre1.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre0_Pre2_Right_Pre2.png"; break;
                case MapStatus.Stop_Pre2_Left_Pre0_Pre2_Right_Pre0_Pre2: uri = "Resources/img/Stop_Pre2_Left_Pre0_Pre2_Right_Pre0_Pre2.png"; break;

                default: uri = "";
                    break;
            }

            return new BitmapImage(new Uri(uri, UriKind.Relative));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
