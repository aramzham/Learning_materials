using System.Windows;

namespace ElementToElementBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SampleTextBlock.FontSize = 2;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SampleTextBlock.FontSize = this.FontSize;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SampleTextBlock.FontSize = 40;
        }
    }
}
