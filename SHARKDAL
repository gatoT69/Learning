using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Acceso_Datos
{
    public class DAL_Clase
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SharkDB;Integrated Security=True;";

        // Método para ejecutar consultas que devuelven resultados

        public DataTable EjecutarConsulta(string consultaSQL, SqlParameter[] parametros, bool esConsultaSQL = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(consultaSQL, connection);

                if (esConsultaSQL)
                {
                    command.CommandType = CommandType.Text;
                }
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                }

                if (parametros != null)
                {
                    command.Parameters.AddRange(parametros);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable resultTable = new DataTable();
                adapter.Fill(resultTable);

                return resultTable;
            }
        }

        // Método para ejecutar comandos que no devuelven resultados 
        public void EjecutarComando(string storedProcedure, SqlParameter[] parametros)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    command.Parameters.AddRange(parametros);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public DataTable ObtenerVentas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Ventas", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable ventasTable = new DataTable();
                adapter.Fill(ventasTable);
                return ventasTable;
            }
        }

        public DataTable ObtenerVentaProducto()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Venta_Producto", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable ventaProductoTable = new DataTable();
                adapter.Fill(ventaProductoTable);
                return ventaProductoTable;
            }
        }

        public DataTable ObtenerClientes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ClienteID, Nombre FROM Cliente", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable clientesTable = new DataTable();
                adapter.Fill(clientesTable);
                return clientesTable;
            }
        }


        public DataTable ObtenerProductos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ProductoID, Nombre FROM Producto", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable productosTable = new DataTable();
                adapter.Fill(productosTable);
                return productosTable;
            }
        }

        public DataTable ObtenerVendedores()
        {

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT VendedorID, Nombre FROM Vendedor", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable vendedorTable = new DataTable();
                adapter.Fill(vendedorTable);
                return vendedorTable;
            }
        }



        public DataTable ObtenerReporteVentasPorVendedor()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ReporteVentasPorVendedor", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable ventasPorVendedorTable = new DataTable();
                adapter.Fill(ventasPorVendedorTable);

                return ventasPorVendedorTable;
            }
        }

        public DataTable ObtenerVentasPorEstado()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ReporteVentasPorEstado", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable VentasPorEstadoTable = new DataTable();
                adapter.Fill(VentasPorEstadoTable);

                return VentasPorEstadoTable;
            }
            
        }

        public DataTable ObtenerVentasPorMunicipio()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ReporteVentasPorMunicipio", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable VentasPorMunicipioTable = new DataTable();
                adapter.Fill(VentasPorMunicipioTable);

                return VentasPorMunicipioTable;
            }

        }


        public decimal ObtenerPrecioProducto(int productoID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Precio FROM Producto WHERE ProductoID = @ProductoID", connection);
                command.Parameters.AddWithValue("@ProductoID", productoID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    throw new Exception("Producto no encontrado o sin precio.");
                }
            }
        }

    }
}
