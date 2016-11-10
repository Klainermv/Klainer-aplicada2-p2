using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class VentasDetalle : ClaseMaestra
    {
        public int VentaDetalleId { get; set; }
        public int VentaId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }

        public VentasDetalle()
        {
            VentaDetalleId = 0;
            VentaId= 0;
            ArticuloId = 0;
            Cantidad = 0;
            Precio = 0;
        }

        public override bool Insertar()
        {
            ConexionDb conexion = new ConexionDb();
            string query = string.Format("Insert into VentasDetalle(VentaId, ArticuloId, Cantidad, Precio) values({0}, {1}, {2}, {3}) select @@identity", VentaId, ArticuloId, Cantidad, Precio);
            VentaDetalleId = Convert.ToInt32(conexion.ObtenerValor(query).ToString());

            return VentaDetalleId > 0;
        }

        public override bool Editar()
        {
            ConexionDb conexion = new ConexionDb();
            bool Retorno = false;
            Retorno = conexion.Ejecutar(string.Format("Update VentasDetalle set VentaId = {0}, ArticuloId {1}, Cantidad = {2}, Precio = {3} where VentaDetalleId = {4}", this.VentaId, this.ArticuloId, this.Cantidad, this.Precio, this.VentaDetalleId));
            return Retorno;
        }
        public override bool Eliminar()
        {
            bool retorno = false;
            ConexionDb conexion = new ConexionDb();
            try
            {
                retorno = conexion.Ejecutar(string.Format("delete from VentasDetalle where VentaDetalleId ={0}", this.VentaDetalleId));

                if (retorno)
                    conexion.Ejecutar("Delete from VentasDetalle where VentaDetalleId=" + this.VentaDetalleId.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();
            ConexionDb conexion = new ConexionDb();
            try
            {
                dt = conexion.ObtenerDatos(String.Format("Select *from VentasDetalle Where VentaDetalleId =" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.VentaDetalleId = (int)dt.Rows[0]["VentaDetalleId"];
                    this.VentaId = (int)dt.Rows[0]["VentaId"];
                    this.ArticuloId = (int)dt.Rows[0]["ArticuloId"];
                    this.Cantidad = (int)dt.Rows[0]["Cantitad"];
                    this.Precio = (float)dt.Rows[0]["Precio"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenFinal = "";
            ConexionDb conexion = new ConexionDb();

            if (!Orden.Equals(""))
                ordenFinal = " Orden by " + Orden;
            return conexion.ObtenerDatos("Select " + Campos + " From VentasDetalle Where " + Condicion + " " + ordenFinal);
        }
    }
}
