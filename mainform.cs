using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PAN_POS_system
{
    public partial class MainForm : Form
    {
        // Cadena de conexión a la base de datos
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=PANPOSSYSTEM;Integrated Security=True;";

        // Variables para almacenar el rol del usuario y el nombre de usuario
        private string userRole;
        private string userUsername;

        // Constructor que acepta nombre de usuario y rol
        public MainForm(string username, string role)
        {
            InitializeComponent(); // Inicializa los componentes del formulario

            // Asigna el rol y el nombre de usuario
            userRole = role;
            userUsername = username;

            // Carga los datos en el DataGrid
            CargarDatos();

            // Actualiza la etiqueta del nombre de usuario
            UpdateUsernameLabel();

            // Configura la interfaz de usuario en función del rol del usuario
            ConfigureUIBasedOnRole();

            // Maneja el evento FormClosed para mostrar el formulario de inicio de sesión al cerrar
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
        }

        // Configura la interfaz de usuario según el rol del usuario
        private void ConfigureUIBasedOnRole()
        {
            if (userRole == "employee")
            {
                // Oculta o deshabilita características de administrador para empleados
                txtNewUserInput.Enabled = false;
                txtNewUserInput.Visible = false;
                txtAddNewPass.Visible = false;
                txtAddNewPass.Enabled = false;
                btnAddNewUser.Enabled = false;
                btnAddNewUser.Visible = false;
                btnDeleteSelectedUser.Enabled = false;
                btnDeleteSelectedUser.Visible = false;
                labRole.Visible = false;
                labRole.Enabled = false;
                cmbRole.Enabled = false;
                cmbRole.Visible = false;
                AddNewPassword.Visible = false;
                AddNewPassword.Enabled = false;
                AddNewUsername.Visible = false;
                AddNewUsername.Enabled = false;
                lblUsers.Enabled = false;
                lblUsers.Visible = false;
            }
            else if (userRole == "admin")
            {
                // Mostrar características de administrador (por defecto están activadas)
                ShowUsersUIAdmin();
            }
        }

        // Actualiza la etiqueta del nombre de usuario en la interfaz de usuario
        private void UpdateUsernameLabel()
        {
            Usernamelabel.Text = $"Welcome, User: {userUsername}";
        }

        // Carga los datos de los usuarios desde la base de datos al DataGridView
        private void CargarDatos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserId, Username, Role FROM Users";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    userDataGrid.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Updating data: " + ex.Message);
            }
        }

        // Maneja el evento Click del botón para agregar un nuevo usuario
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            string username = txtNewUserInput.Text;
            string password = txtAddNewPass.Text;
            string role = cmbRole.SelectedItem.ToString();

            // Verifica si todos los campos están llenos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Fill all the blanks.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Añade parámetros al comando SQL
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@role", role);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Database Successfully updated");
                            CargarDatos(); // Actualiza la vista después de agregar
                        }
                        else
                        {
                            MessageBox.Show("Error updating the Table");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Maneja el evento Click del botón para eliminar el usuario seleccionado
        private void btnDeleteSelectedUser_Click(object sender, EventArgs e)
        {
            if (userDataGrid.SelectedRows.Count > 0)
            {
                // Obtiene el ID del usuario seleccionado
                int id = Convert.ToInt32(userDataGrid.SelectedRows[0].Cells["UserId"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Users WHERE UserId = @UserId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@UserId", id);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    CargarDatos(); // Actualiza la vista después de eliminar
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Deleting Data: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please, select a row");
            }
        }

        // Muestra la interfaz de usuario para administración de usuarios
        private void ShowUsersUIAdmin()
        {
            UsersPanel.Enabled = true;
            UsersPanel.Visible = true;
        }

        // Maneja el evento FormClosed para mostrar el formulario de inicio de sesión al cerrar el formulario principal
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}


