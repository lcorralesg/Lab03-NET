using System.Data.SqlClient;
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
using Lab03_Console;

namespace Lab03_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            string connectionString = "Server=(localdb)\\local;Database=Tecsup2024DB;Integrated Security=true;";
            List<Estudiante> estudiantes = new List<Estudiante>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estudiante estudiante = new Estudiante();
                    estudiante.Id = reader.GetInt32(0);
                    estudiante.Nombre = reader.GetString(1);
                    estudiante.Apellido = reader.GetString(2);
                    estudiantes.Add(estudiante);
                }
                connection.Close();
            }
            dgEstudiantes.ItemsSource = estudiantes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=(localdb)\\local;Database=Tecsup2024DB;Integrated Security=true;";
            List<Estudiante> estudiantes = new List<Estudiante>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM Students WHERE FirstName LIKE '%{txtBuscar.Text}%'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estudiante estudiante = new Estudiante();
                    estudiante.Id = reader.GetInt32(0);
                    estudiante.Nombre = reader.GetString(1);
                    estudiante.Apellido = reader.GetString(2);
                    estudiantes.Add(estudiante);
                }
                connection.Close();
            }
            dgEstudiantes.ItemsSource = estudiantes;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}