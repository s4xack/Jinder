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
    /// Interaction logic for SummaryControl.xaml
    /// </summary>
    public partial class SummaryControl : UserControl
    {
        public SummaryControl()
        {
            InitializeComponent();
        }

        public static DependencyProperty SummaryProperty = DependencyProperty.Register("Summary", typeof(SummaryDto)
            ,typeof(SummaryControl), new PropertyMetadata(null));
        public SummaryDto Summary
        {
            get => (SummaryDto) GetValue(SummaryProperty);
            set => SetValue(SummaryProperty, value);
        }
    }
}
