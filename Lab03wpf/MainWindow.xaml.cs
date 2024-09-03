using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab03wpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=DESKTOP-OT3T8Q7; Database=Tecsup2023DB; Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = searchTextBox.Text;
            SearchDataByName(searchQuery);
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error de Conexion");
                }
            }
        }

        private void SearchDataByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Students WHERE FirstName LIKE @name", connection);
                    command.Parameters.AddWithValue("@name", "%" + name + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error de Conexion");
                }
            }
        }
    }
}
