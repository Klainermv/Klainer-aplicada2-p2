using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Articulos : ClaseMaestra
    {
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public string Existencia { get; set; }
        public float Precio { get; set; }

        public Articulos()
        {
            ArticuloId = 0;
            Descripcion = "";
            Existencia = "";
            Precio = 0f;
        }

        public override bool Insertar()
        {
            ConexionDb conexion = new ConexionDb();
            string query = string.Format("Insert into Articulos(Descripcion, Existencia, Precio) values('{0}', '{1}'{2}) select @@identity", Descripcion, Existencia, Precio);
            ArticuloId = Convert.ToInt32(conexion.ObtenerValor(query).ToString());

            return ArticuloId > 0;
        }

        public override bool Editar()
        {
            ConexionDb conexion = new ConexionDb();
            bool Retorno = false;
            Retorno = conexion.Ejecutar(string.Format("Update Articulos set Descripcion = '{0}', Existencia '{1}', Precio {2} where ArticuloId = {3}", this.Descripcion, this.Existencia, this.Precio, this.ArticuloId));
            return Retorno;
        }

        public override bool Eliminar()
        {
            bool retorno = false;
            ConexionDb conexion = new ConexionDb();
            try
            {
                retorno = conexion.Ejecutar(string.Format("delete from Articulos where ArticuloId =" + this.ArticuloId));

                if (retorno)
                    conexion.Ejecutar("Delete from SolicituDetalle where IdMaerial=" + this.ArticuloId.ToString());
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
                dt = conexion.ObtenerDatos(String.Format("Select *from Articulos Where ArticuloId =" + IdBuscado));
                if (dt.Rows.Count > 0)
                {
                    this.ArticuloId = (int)dt.Rows[0]["IdArticulo"];
                    this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    this.Existencia = dt.Rows[0]["Existencia"].ToString();
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
            return conexion.ObtenerDatos("Select " + Campos + " From Articulos Where " + Condicion + " " + ordenFinal);
        }
    }
}
