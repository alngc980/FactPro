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

namespace Facturador
{
    public partial class GeneradorTXT : Form
    {
        public GeneradorTXT()
        {
            InitializeComponent();
        }

        CapaSunat. conexion cn = new conexion();
        SqlConnection scn = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand scmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string rpta = "";
        string directorio = "", directoriotxt = "";
        string Host = "";
        string Rucc = "";
        string Afectacion = "0";
        private void GeneradorTXT_Load(object sender, EventArgs e)
        {
            Carga_empresa(cbempresa);
            lblboleta.Text = "";
            lblfactura.Text = "";
        }

        public void Carga_empresa(ComboBox cb)
        {
            try
            {
                scn = cn.conectar();
                scn.Open();

                SqlCommand comand;
                comand = new SqlCommand("select id, ruc + '-' + razon_social Descripcion from Empresa where bEstado = 1", scn);
                sda = new SqlDataAdapter(comand);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                cb.DataSource = dt;
                cb.DisplayMember = "Descripcion";
                cb.ValueMember = "id";

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CargarBoletas()
        {
            dgvboleta.Rows.Clear();
            scn = cn.conectar();
            scn.Open();
            DateTime fec = Convert.ToDateTime(dtpfecha.Text);
            string fecha = fec.ToString("yyyyMMdd");
            SqlCommand comand = new SqlCommand("select DISTINCT CONVERT(date,dFechaHora)FechaHora,'01' tipodoc,cSerie+'-'+cCorrelativo SerieCorrelativo,cClienteDoc,dMontoTotal,bAnulado,nIdVenta from VentasFact " +
                "where nTipoDoc= 1 and nIdEmpresa = " + cbempresa.SelectedValue.ToString() + " and CONVERT(date,dFechaHora)='" + fecha + "'", scn);

            SqlDataAdapter sda = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblboleta.Text = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string V1 = fec.ToString("yyyyMMdd");
                    string V2 = dt.Rows[i][1].ToString();
                    string V3 = dt.Rows[i][2].ToString();
                    string V4 = dt.Rows[i][3].ToString();
                    string V5 = dt.Rows[i][4].ToString();
                    string V6 = dt.Rows[i][5].ToString();
                    string V7 = dt.Rows[i][6].ToString();
                    dgvboleta.Rows.Add(V1, V2, V3, V4, V5, V6, V7);
                }
            }
            else
            {
                dgvboleta.Rows.Clear();
                lblboleta.Text = "NO HAY REGISTRO DE BOLETAS";
            }
        }

        public void CargarFacturas()
        {
            dgvfactura.Rows.Clear();
            scn = cn.conectar();
            scn.Open();
            DateTime fec = Convert.ToDateTime(dtpfecha.Text);
            string fecha = fec.ToString("yyyyMMdd");
            SqlCommand comand = new SqlCommand("select DISTINCT cSerie+'-'+cCorrelativo SerieCorrelativo,cClienteDoc,cClienteRazonSocial,dMontoTotal,bAnulado, nIdVenta from VentasFact " +
                "where nTipoDoc= 2 and nIdEmpresa = " + cbempresa.SelectedValue.ToString() + " and CONVERT(date,dFechaHora)='" + fecha + "'", scn);

            SqlDataAdapter sda = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblfactura.Text = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string V1 = dt.Rows[i][0].ToString();
                    string V2 = dt.Rows[i][1].ToString();
                    string V3 = dt.Rows[i][2].ToString();
                    string V4 = dt.Rows[i][3].ToString();
                    string V5 = dt.Rows[i][4].ToString();
                    string V6 = dt.Rows[i][5].ToString();
                    dgvfactura.Rows.Add(V1, V2, V3, V4, V5, V6);
                }
            }
            else
            {
                dgvfactura.Rows.Clear();
                lblfactura.Text = "NO HAY REGISTRO DE FACTURAS";
            }
        }

        private void btncargar_Click(object sender, EventArgs e)
        {

        }

        private void cbempresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvboleta.Rows.Clear();
            dgvfactura.Rows.Clear();

            int i = 0;
            string s = cbempresa.SelectedValue.ToString();
            if (int.TryParse(s, out i))
            {
                CargarBoletas();
                CargarFacturas();
                CargaDirectorio();
            }
        }

        private void dtpfecha_ValueChanged(object sender, EventArgs e)
        {
            dgvboleta.Rows.Clear();
            dgvfactura.Rows.Clear();

            CargarBoletas();
            CargarFacturas();
        }

        private void btndirectorio_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"" + directorio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void CargaDirectorio()
        {
            dgvfactura.Rows.Clear();
            scn = cn.conectar();
            scn.Open();
            DateTime fec = Convert.ToDateTime(dtpfecha.Text);
            string fecha = fec.ToString("yyyyMMdd");
            SqlCommand comand = new SqlCommand("select ruc, carpetaTrabajo, carpetaTxt, nAfectacion from Empresa where id = " + cbempresa.SelectedValue.ToString(), scn);

            SqlDataAdapter sda = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblfactura.Text = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string V1 = dt.Rows[i][0].ToString();
                    string V2 = dt.Rows[i][1].ToString();
                    string V3 = dt.Rows[i][2].ToString();
                    string V4 = dt.Rows[i][3].ToString();
                    Rucc = V1;
                    directorio = V2;
                    directoriotxt = V3;
                    Afectacion = V4;
                }
            }
            else
            {
                dgvfactura.Rows.Clear();
                lblfactura.Text = "NO HAY REGISTRO DE FACTURAS";
            }
        }

        Gen gen = new Gen();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"" + directoriotxt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvfactura_DoubleClick(object sender, EventArgs e)
        {
            CambioFecha cf = new CambioFecha();
            cf.Empresa = cbempresa.SelectedValue.ToString();
            cf.label1.Text = Convert.ToString(this.dgvfactura.CurrentRow.Cells[0].Value);
            cf.IdVenta = Convert.ToString(this.dgvfactura.CurrentRow.Cells[5].Value);
            cf.CargarFecha();
            cf.ShowDialog();

            dgvboleta.Rows.Clear();
            dgvfactura.Rows.Clear();
            CargarBoletas();
            CargarFacturas();
        }

        private void dtpfecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btngenerar.PerformClick();
            }
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {
            if (dgvboleta.Rows.Count > 0)
            {
                gen.RUC = Rucc;
                gen.Ruta = directoriotxt;
                gen.GenerarResumenDiario(dtpfecha, dgvboleta, Afectacion);
            }

            if (dgvfactura.Rows.Count > 0)
            {
                gen.RUC = Rucc;
                gen.Ruta = directoriotxt;
                foreach (DataGridViewRow fila in dgvfactura.Rows)
                {
                    gen.GenerarTxt_Venta(cbempresa.SelectedValue.ToString(), fila.Cells[5].Value.ToString());
                }
                MessageBox.Show("TXT de facturas han sido Generados", "Mensaje");
            }
        }
    }
}
