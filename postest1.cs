using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<Usuario> TEST = new CN_usuario().Listar();

            Usuario oUsuario = new CN_usuario().Listar().Where(u => u.Documento == textBox2.Text && u.Clave == textBox1.Text).FirstOrDefault();

            if (oUsuario != null) {

                Inicio form = new Inicio();

                form.Show();

                this.Hide();

                form.FormClosing += frm_Closing;

            }

            else {

                MessageBox.Show("Usuario no encontrado", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

          
        }

        private void frm_Closing(object sender, FormClosingEventArgs e) {

            textBox1.Text = "";
            textBox2.Text = "";

            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
