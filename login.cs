using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PAN_POS_system
{
    public partial class Login : Form
    {
        // Cadena de conexión a la base de datos
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=PANPOSSYSTEM;Integrated Security=True;";

        // Constructor de la clase Login
        public Login()
        {
            InitializeComponent(); // Inicializa los componentes del formulario
        }

        // Maneja el evento Click del botón Aceptar
        public void Aceptar_Click(object sender, EventArgs e)
        {
            // Obtiene el nombre de usuario y la contraseña de los controles de entrada
            string username = txtUser.Text;
            string password = txtPassword.Text;

            // Valida el usuario y obtiene el rol si es válido
            if (ValidateUser(username, password, out string role))
            {
                // Muestra un mensaje de éxito y abre el formulario principal
                MessageBox.Show($"Login successful! Role: {role}");
                OpenMainForm(username, role);
            }
            else
            {
                // Muestra un mensaje de error si el usuario o la contraseña son incorrectos
                MessageBox.Show("Invalid username or password.");
            }
        }

        // Valida el nombre de usuario y la contraseña contra la base de datos
        private bool ValidateUser(string username, string password, out string role)
        {
            role = null; // Inicializa el rol como null

            // Establece la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener el rol del usuario
                string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

                // Crea el comando SQL con parámetros
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Añade parámetros al comando SQL
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    // Abre la conexión a la base de datos
                    connection.Open();

                    // Ejecuta la consulta y obtiene el resultado
                    object result = command.ExecuteScalar();

                    // Si el resultado no es null, el usuario es válido
                    if (result != null)
                    {
                        role = result.ToString(); // Asigna el rol al parámetro de salida
                        return true; // Usuario válido
                    }
                }
            }
            return false; // Usuario no válido
        }

        // Abre el formulario principal y pasa el nombre de usuario y el rol
        private void OpenMainForm(string username, string role)
        {
            // Crea una instancia del formulario principal
            MainForm mainForm = new MainForm(username, role);

            // Muestra el formulario principal
            mainForm.Show();

            // Opcionalmente oculta el formulario de inicio de sesión
            this.Hide();
        }

        // Maneja el evento Click del botón Salir
        private void Salir_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual
            this.Close();
        }
    }
}
