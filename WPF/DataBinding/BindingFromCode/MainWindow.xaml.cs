using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BindingFromCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var binding = new Binding
            {
                ElementName = "Slider",
                Path = new PropertyPath("Value"),
                Mode = BindingMode.OneWay
            };
            SampleTextBlock.SetBinding(TextBlock.FontSizeProperty, binding);
        }
    }
}
