using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Capa_Negocio;
using Capa_Acceso_Datos;

namespace SharkPOS
{
    public partial class SharkUI : Form
    {


        private Negocio_Clase bll = new Negocio_Clase();


        public SharkUI()
        {
            InitializeComponent();

            CargarVentasEnGrid();

            CargarVentaProducto();

            CargarClientesEnComboBox();

            CargarVendedoresEnComboBox();

            CargarProductosEnComboBox();
        }

        private void btnObtenerVenta_Click(object sender, EventArgs e)
        {
            DataTable ventas = bll.ObtenerVentasPorVendedor();

            MostrarReporteVentasPorVendedor();

            MostrarReporteVentasPorEstado();

            MostrarReporteVentasPorMunicipio();
        }

        private void btnInsertarVenta_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor, ingrese una cantidad.");
                return;
            }

            DateTime fecha = DateTime.Now;
           int clienteID = (int)cmbClientes.SelectedValue;  
            int vendedorID = (int)cmbVendedor.SelectedValue; 
            

            DataTable productos = new DataTable();
            productos.Columns.Add("ProductoID", typeof(int));
            productos.Columns.Add("Cantidad", typeof(int));
            productos.Columns.Add("PrecioUnitario", typeof(decimal));

          

            int productoID = (int)cmbProducto.SelectedValue;
            decimal precioUnitario = bll.ObtenerPrecioProducto(productoID);

       

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("Por favor, ingrese un valor numérico válido para la cantidad.");
                return;
            }
            productos.Rows.Add((int)cmbProducto.SelectedValue, cantidad, precioUnitario);

            bll.InsertarVenta(fecha, clienteID, vendedorID, productos);
            MessageBox.Show("Venta insertada exitosamente");

            CargarVentasEnGrid();

            CargarVentaProducto();
        }


        private void CargarVentasEnGrid()
        {
            DataTable ventas = bll.ObtenerTodasLasVentas(); 
            dgVentas.DataSource = ventas;  
        }

        private void CargarVentaProducto()
        {

            DataTable ventaProducto = bll.ObetenerVentasProducto();
            dgVentaProducto.DataSource = ventaProducto;
        }




        private void CargarClientesEnComboBox()
        {
            DataTable clientes = bll.ObtenerListaDeClientes();  
            cmbClientes.DataSource = clientes;
            cmbClientes.DisplayMember = "Nombre";  
            cmbClientes.ValueMember = "ClienteID";  
        }

        private void CargarVendedoresEnComboBox()
        {
            DataTable vendedores = bll.ObtenerListaDeVendedores();  
            cmbVendedor.DataSource = vendedores;
            cmbVendedor.DisplayMember = "Nombre";  
            cmbVendedor.ValueMember = "VendedorID";  
        }

        private void CargarProductosEnComboBox()
        {
            DataTable productos = bll.ObtenerListaProductos();  
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Nombre";  
            cmbProducto.ValueMember = "ProductoID";  
        }



  



        private void MostrarReporteVentasPorVendedor()
        {
            
            DataTable reporte = bll.ObtenerReporteVentasPorVendedor();

            
            dgReporteVentasxVendedor.DataSource = reporte;
        }

        private void MostrarReporteVentasPorEstado()
        {
            
            DataTable reporte = bll.ObtenerReporteVentasPorEstado();

            
            dgVentasPorEstado.DataSource = reporte;
        }

        private void MostrarReporteVentasPorMunicipio()
        {
            DataTable reporte = bll.ObtenerReporteVentasPorMunicipio();

            dgVentasPorMunicipio.DataSource = reporte;

        }
 
    }
}
