using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Testing
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=TestingDB;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Personas";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingrese un nombre.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Personas (Nombre) VALUES (@Nombre)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Dato agregado correctamente.");
                        CargarDatos(); // Actualiza la vista después de agregar
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el dato.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el dato: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Personas WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el dato: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar.");
            }
        }

    }
}
