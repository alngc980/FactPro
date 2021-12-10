//using Conexiones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VB;
using CapaSUNAT;

namespace Facturador
{
    public partial class CambioFecha : Form
    {
        public CambioFecha()
        {
            InitializeComponent();
        }
        CapaSUNAT.ConexionN cn = new CapaSUNAT.ConexionN();
        SqlConnection scn = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand scmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public string Empresa, IdVenta, Fecha;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Gen asd = new Gen();
        private void btnguardar_Click(object sender, EventArgs e)
        {
            asd.ModificarFecha(dtpfecha.Text, Empresa, IdVenta);
        }

        private void CambioFecha_Load(object sender, EventArgs e)
        {
            dtpfecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpfecha.Format = DateTimePickerFormat.Custom;
        }

        public void CargarFecha()
        {
            scn = cn.conectar();
            scn.Open();
            SqlCommand comand = new SqlCommand("SELECT top 1 dFechaHora FROM VentasFact where nIdEmpresa = " + Empresa + " and nIdVenta = " + IdVenta, scn);

            SqlDataAdapter sda = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dtpfecha.Text = dt.Rows[0][0].ToString();
            }
        }
    }
}
