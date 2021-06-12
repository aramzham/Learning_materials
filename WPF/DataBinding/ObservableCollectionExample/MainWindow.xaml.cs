using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ObservableCollectionExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> observable = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();

            var debut = new[] { "One", "Two", "Three" };
            foreach (var x in debut)
            {
                observable.Add(x);
            }

            MyListBox.ItemsSource = observable;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //with List you won't see added items on screen as List doesn't have an event on added item
            observable.Add(DateTime.Now.ToLongTimeString());
        }
    }
}
