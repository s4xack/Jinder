using System.Windows;
using System.Windows.Controls;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for VacancyControl.xaml
    /// </summary>
    public partial class VacancyControl : UserControl
    {
        public VacancyControl()
        {
            InitializeComponent();
        }

        public static DependencyProperty VacancyProperty = DependencyProperty.Register("Vacancy", typeof(VacancyDto)
            , typeof(VacancyControl), new PropertyMetadata(null));

        public VacancyDto Vacancy
        {
            get => (VacancyDto) GetValue(VacancyProperty);
            set => SetValue(VacancyProperty, value);
        }
    }
}
