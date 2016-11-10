using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Ventas : ClaseMaestra
    {

        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public float Monto { get; set; }

        private string tabla;
        public Ventas()
        {
            VentaId = 0;
            Fecha = DateTime.Now;
            Monto = 0f;
            tabla = "Ventas";

        }
        public override bool Insertar()
        {
            ConexionDb conexion = new ConexionDb();
            string query = string.Format("insert into {0}(Fecha, Monto) values({1},{2}) select @@identity", tabla, Fecha.ToString("yyy-MM-dd"), Monto);
            VentaId = Convert.ToInt32(conexion.ObtenerValor(query).ToString());
            return VentaId > 0;
        }

        public override bool Editar()
        {
            ConexionDb conexion = new ConexionDb();

            bool Retorno = false;
            Retorno = conexion.Ejecutar(String.Format("Update {0} set Fecha = {1}, Monto = {2} where VentaId = {3}", this.tabla, this.Fecha, this.Monto, this.VentaId));
            return Retorno;
        }

        public override bool Eliminar()
        {
            ConexionDb conexion = new ConexionDb();

            bool retorno = false;
            retorno = conexion.Ejecutar(String.Format("Delete from {0} where VentaId = {1}", this.tabla, this.VentaId));
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            ConexionDb conexion = new ConexionDb();

            DataTable dt = new DataTable();

            dt = conexion.ObtenerDatos(string.Format("Select * from {0} where ArticuloId = {1}", tabla, IdBuscado));
            if (dt.Rows.Count > 0)
            {
                this.VentaId = (int)dt.Rows[0]["VentaId"];
                this.Fecha = Convert.ToDateTime(dt.Rows[0]["Fecha"].ToString());
                this.Monto = (float)dt.Rows[0]["Monto"];
            }
            return dt.Rows.Count > 0;
        }


        public override DataTable Listado(string Campos = "*", string Condicion = "1=1", string Orden = "desc")
        {
            ConexionDb conexion = new ConexionDb();
            return conexion.ObtenerDatos("Select" + Campos + "from Ventas where " + Condicion + " order by VentaId " + Orden);
        }
    }
}

