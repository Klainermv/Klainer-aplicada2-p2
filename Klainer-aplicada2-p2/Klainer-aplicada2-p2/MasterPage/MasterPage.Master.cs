using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Klainer_aplicada2_p2.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        float monto = 0f;
        Articulos a = new Articulos();
        Ventas v = new Ventas();
        VentasDetalle vd = new VentasDetalle();
        DataTable dt;
        DataColumn dc;
        DataRow dr;

        public Registros
            ()
        {
            dt = new DataTable("Articulos");

            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Articulos";
            dc.Caption = "Articulo";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Descripcion";
            dc.Caption = "Descripcion";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Existencia";
            dc.Caption = "Existencia";
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Precio";
            dc.Caption = "Precio";
            dt.Columns.Add(dc);

            dr = dt.NewRow();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewventas.DataSource = dt;
        }

        protected void Buttonadd_Click(object sender, EventArgs e)
        {
            dr["Articulo"] = TextBoxaarticulo.Text;
            dr["Cantidad"] = TextBoxcantidad.Text;
            dr["Precio"] = float.Parse(TextBoxprecio.Text);
            float inventario = (Convert.ToInt32(TextBoxcantidad.Text) * float.Parse(TextBoxprecio.Text));
            dr["Inventario"] = inventario;
            monto = monto + inventario;
            dt.Rows.Add(dr);
            GridViewventas.DataBind();

            /*m.Descripcion = TextBoxdescripcion.Text;
            m.Precio = float.Parse(TextBoxprecio.Text);
            m.Insertar();*/

        }

        protected void Buttonguardar_Click(object sender, EventArgs e)
        {
            v.Fecha = Convert.ToDateTime(TextBoxfecha.Text);
            v.Monto = 100f;
            v.Insertar();//
            //Labeltotal.Text = to;
        }

        protected void Buttoneliminar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiarbutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
