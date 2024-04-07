// See https://aka.ms/new-console-template for more information
//Consola

using Lab03_Console;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

class Program
{
    string connectionString = "Server=(localdb)\\local;Database=Tecsup2024DB;Integrated Security=true;";

    public DataTable GetEstudiantesDataTable()
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Students";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dt);
            connection.Close();
        }
        return dt;
    }

    public List<Estudiante> GetEstudiantesList()
    {
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
        return estudiantes;
    }

    static void Main(string[] args)
    {
        // Crear una instancia de la clase Program
        Program program = new Program();
        // Imprimir base de datos conectada si se conecta correctamente
        Console.WriteLine("Lista usando DataTable");
        DataTable dt = program.GetEstudiantesDataTable();
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"{row[0]} {row[1]} {row[2]}");
        }
        Console.WriteLine("Lista usando una lista de objetos");
        List<Estudiante> estudiantes = program.GetEstudiantesList();
        foreach (Estudiante estudiante in estudiantes)
        {
            Console.WriteLine($"{estudiante.Id} {estudiante.Nombre} {estudiante.Apellido}");
        }
    }
}
