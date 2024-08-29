using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Acceso_Datos;

namespace Capa_Negocio
{
    public class Negocio_Clase
    {
        private DAL_Clase dal = new DAL_Clase();

        public DataTable ObtenerVentasPorVendedor()
        {
            return dal.EjecutarConsulta("ReporteVentasPorVendedor", null);
        }

        public void InsertarVenta(DateTime fecha, int clienteID, int vendedorID, DataTable productos)
        {
            if (!ClienteExiste(clienteID))
            {
                throw new Exception("El ClienteID especificado no existe.");
            }

            // Verifica si los ProductoID existen
            foreach (DataRow row in productos.Rows)
            {
                int productoID = (int)row["ProductoID"];
                if (!ProductoExiste(productoID))
                {
                    throw new Exception($"El ProductoID {productoID} especificado no existe.");
                }
            }

            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@Fecha", fecha),
        new SqlParameter("@ClienteID", clienteID),
        new SqlParameter("@VendedorID", vendedorID),
        new SqlParameter("@Productos", productos)
            };

            dal.EjecutarComando("InsertarVenta", parametros);
        }

        private bool ClienteExiste(int clienteID)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@ClienteID", clienteID)
            };

            DataTable dt = dal.EjecutarConsulta("SELECT COUNT(*) FROM Cliente WHERE ClienteID = @ClienteID", parametros, true);

            return (int)dt.Rows[0][0] > 0;
        }

        private bool ProductoExiste(int productoID)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@ProductoID", productoID)
            };

            DataTable dt = dal.EjecutarConsulta("SELECT COUNT(*) FROM Producto WHERE ProductoID = @ProductoID", parametros, true);

            return (int)dt.Rows[0][0] > 0;
        }



        public DataTable ObtenerTodasLasVentas()
        {
            return dal.ObtenerVentas();  // Llama al método de la DAL
        }

        public DataTable ObetenerVentasProducto()
        {

            return dal.ObtenerVentaProducto();
        }


        public DataTable ObtenerListaDeClientes()
        {
            return dal.ObtenerClientes();
        }

        public DataTable ObtenerListaDeVendedores()
        {
            return dal.ObtenerVendedores();
        }


        public DataTable ObtenerReporteVentasPorVendedor()
        {
            return dal.ObtenerReporteVentasPorVendedor();
        }

        public DataTable ObtenerReporteVentasPorEstado()
        {
            return dal.ObtenerVentasPorEstado();
        }

        public DataTable ObtenerReporteVentasPorMunicipio()
        {
            return dal.ObtenerVentasPorMunicipio();
        }


        public DataTable ObtenerListaProductos()
        {
            return dal.ObtenerProductos();
        }



        public decimal ObtenerPrecioProducto(int productoID)
        {
            return dal.ObtenerPrecioProducto(productoID);
        }
    }
}
