using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        public ProfileControl()
        {
            InitializeComponent();
        }

        public static DependencyProperty UserProperty = DependencyProperty.Register("User", typeof(UserDto)
            ,typeof(UserControl), new PropertyMetadata(null));
        public UserDto User
        {
            get => (UserDto) GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }
    }
}
