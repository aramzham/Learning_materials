using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connectionString = @"data source = (local)\sqlexpress; integrated security = true; initial catalog = shopDB;";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Author_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("XAML design director: Vanik Hakobyan");
        }


        private void Show_all_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAll.Items.Count != 0 && ListBoxAll.Visibility != Visibility.Collapsed)
            {
                ListBoxAll.Visibility = Visibility.Collapsed;
                ListBoxAll.ItemsSource = null;
                ListBoxAll.Items.Clear();
            }
            else
            {
                ListBoxAll.Visibility = Visibility.Visible;
                //using (var connection = new SqlConnection(connectionString))
                //{
                //    var command = new SqlCommand("SELECT * FROM [dbo].[Table]", connection);

                //    connection.Open();

                //    var reader = command.ExecuteReader();

                //    while (reader.Read())
                //    {
                //        ListBoxAll.Items.Add(reader[1]);
                //    }
                //    reader.Close();
                //}
                var customers = new DataTable("Customers");
                var adapter = new SqlDataAdapter("select * from customers", connectionString);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(customers);

                var listName = new List<string>();

                for (int i = 0; i < customers.Rows.Count; i++)
                {
                    listName.Add($"{customers.Rows[i]["FName"]} {customers.Rows[i]["LName"]}");
                }

                ListBoxAll.ItemsSource = listName.Distinct();
            }
        }

        private void ShowByNumber_Click(object sender, RoutedEventArgs e)
        {
            var myTable = new DataTable();
            var adapter = new SqlDataAdapter("select * from Customers", connectionString);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(myTable);

            var customerRow = myTable.Rows.Find(int.Parse(IdTextBox.Text));
            FirstLastNAme.Text = $"{customerRow[1]} {customerRow[2]}";
            AboutIn.Text = $"Address: {customerRow[4]}\nCity: {customerRow[6]}\nPhone: {customerRow[7]}\nDate in system: {customerRow[8]}";
            //var playerId = int.Parse(IdTextBox.Text);

            //using (var connection = new SqlConnection(connectionString))
            //{


            //    SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Table] WHERE ID=@id", connection);
            //    command.Parameters.AddWithValue("id", playerId);
            //    connection.Open();

            //    var reader = command.ExecuteReader();
            //    reader.Read();

            //    FirstLastNAme.Text = reader[1].ToString();
            //    AboutIn.Text = reader[2].ToString();

            //    reader.Close();
            //}
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(IdTextBox.Text)) MessageBox.Show("Enter an ID");
            else
            {
                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand();
                if (FirstLastNAme.Text.Count(c => c == ' ') > 1)
                {
                    MessageBox.Show("Not a valid format");
                    return;
                }
                else if (!FirstLastNAme.Text.Contains(' '))
                {
                    command = new SqlCommand($"update customers set fname = '{FirstLastNAme.Text}' where customerno = {IdTextBox.Text}", connection);
                }
                else if (FirstLastNAme.Text.Count(c => c == ' ') == 1)
                {
                    var split = FirstLastNAme.Text.Split(' ');
                    command = new SqlCommand($"update customers set fname = '{split[0]}', lname = '{split[1]}' where customerno = {IdTextBox.Text}", connection);
                }

                try
                {
                    connection.Open();

                    command.Transaction = connection.BeginTransaction();

                    command.ExecuteNonQuery();

                    command.Transaction.Commit();
                    MessageBox.Show("Transaction commited");
                }
                catch (Exception)
                {
                    command.Transaction.Rollback();
                    MessageBox.Show("transaction rolled back");
                }
            }
        }
    }
}
