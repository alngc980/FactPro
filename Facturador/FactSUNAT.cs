using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices; // Recuerden agregar la referencia "Microsoft.VisualBasic".
using CapaSunar;
using System.Data.SqlClient;

namespace Facturador
{
    public partial class FactSUNAT : Form
    {
        public FactSUNAT()
        {
            InitializeComponent();
        }
        Computer mycomputer = new Computer(); // Así accederemos al "FileSystem".
        string estadodirect = "0";
        string est_temp = "1";  //Tiempo corriendo
        int sec1 = 0, sec2 = 0;
        string caso;


        public string directorio = "", directoriotxt = "";
        public string HostLink = "";
        public string Rucc = "";

        ///--------------VAR GENERAR XML MASIVO---------------------------------------------------        
        int gfilas = -1, gfilaactual = -1;

        private void btngenM_Click(object sender, EventArgs e)
        {
            ///------------------------------------------------------------------------------------------------------------------------------
            try
            {
                int filas = dgvlistaComprobantes.Rows.Count;

                txtmensaje.Text = "MENSAJE: GENERANDO XML MASIVO!";
                if (dgvlistaComprobantes.Rows.Count > 0)
                {
                    string ruc;
                    string tipodoc;
                    string seriecorrelativo;
                    foreach (DataGridViewRow fila in dgvlistaComprobantes.Rows)
                    {
                        ruc = Convert.ToString(fila.Cells[0].Value);
                        tipodoc = Convert.ToString(fila.Cells[1].Value);
                        seriecorrelativo = Convert.ToString(fila.Cells[2].Value);

                        GenerarYFirmarXML(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                        Listar_comprobantes(dgvlistaComprobantes, HostLink);
                    }

                }
                Contar_Comprobantes();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE: " + ex;
            }
        }

        public static void Listar_comprobantes(DataGridView dgv, string host)
        {
            try
            {
                //txtmensaje.Text = "MENSAJE : PROCESANDO LISTA!";
                string web = host + "/api/ActualizarPantalla.htm";
                WebRequest request = WebRequest.Create(web);
                request.Method = "POST";
                string postData = "{\"txtSecuencia\":\"" + 000 + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                string respu;

                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    respu = responseFromServer.ToString();
                }
                //Cargando la data en la grilla
                dgv.DataSource = GetDataTableFromJsonString(respu);

                if (dgv.Rows.Count > 0)
                {
                    //ocultar Campos
                    dgv.Columns[7].Visible = false;
                    dgv.Columns[8].Visible = true;
                    dgv.Columns[9].Visible = false;
                    dgv.Columns[10].Visible = false;

                    //SIGNANDO TITULOSs
                    dgv.Columns[0].HeaderText = "RUC";
                    dgv.Columns[1].HeaderText = "TP";
                    dgv.Columns[2].HeaderText = "NUMERACION";
                    dgv.Columns[3].HeaderText = "CARGA";
                    dgv.Columns[4].HeaderText = "GEN XML";
                    dgv.Columns[5].HeaderText = "ENVIO SUNAT";
                    dgv.Columns[6].HeaderText = "OBSERVACION";
                    dgv.Columns[7].HeaderText = "NOMBRE ARCHIVO";
                    dgv.Columns[8].HeaderText = "INT SITU";
                    dgv.Columns[9].HeaderText = "TIPO ARCHIVO";
                    dgv.Columns[10].HeaderText = "FIRMA";

                    //asignando ancho de las columnas
                    dgv.Columns[0].Width = 75;
                    dgv.Columns[1].Width = 30;
                    dgv.Columns[2].Width = 90;
                    dgv.Columns[3].Width = 80;
                    dgv.Columns[4].Width = 120;
                    dgv.Columns[5].Width = 120;
                    dgv.Columns[6].Width = 200;
                    dgv.Columns[7].Width = 180;
                    dgv.Columns[8].Width = 80;

                    dgv.AllowUserToResizeColumns = false;
                    dgv.AllowUserToResizeRows = false;
                }
                response.Close();
                //txtmensaje.Text = "MENSAJE : PROCESO LISTA CONCLUIDO!";
            }
            catch (Exception ex)
            {
                //txtmensaje.Text = "MENSAJE: " + ex;
            }
        }
        private void FactSUNAT_Load(object sender, EventArgs e)
        {
            txtmensaje.Text = "MENSAJE: FORMULARIO CARGADO!";
            //temporizador.Start();

            GeneradorTXT gen = new GeneradorTXT();
            gen.Carga_empresa(comboBox1);

            CargaDirectorio();
            //Temp2.Start();
        }
        private void btnactualizarlista_Click(object sender, EventArgs e)
        {
            try
            {
                txtmensaje.Text = "MENSAJE: ACTUALIZANDO LISTA";
                Listar_comprobantes(dgvlistaComprobantes, HostLink);
                Contar_Comprobantes();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE: " + ex;
            }
        }

        private void btnenviarM_Click(object sender, EventArgs e)
        {
            try
            {
                txtmensaje.Text = "MENSAJE: INFORMANDO COMPROBANTES A SUNAT!";
                if (dgvlistaComprobantes.Rows.Count > 0)
                {
                    string ruc;
                    string tipodoc;
                    string seriecorrelativo;
                    foreach (DataGridViewRow fila in dgvlistaComprobantes.Rows)
                    {
                        ruc = Convert.ToString(fila.Cells[0].Value);
                        tipodoc = Convert.ToString(fila.Cells[1].Value);
                        seriecorrelativo = Convert.ToString(fila.Cells[2].Value);

                        EnviarXMLSunat(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                        Listar_comprobantes(dgvlistaComprobantes, HostLink);
                    }
                }
                Contar_Comprobantes();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE: " + ex;
            }
        }
        public void Contar_Comprobantes()
        {
            try
            {
                decimal total1 = 0;
                txt1.Text = Convert.ToString(0);
                foreach (DataGridViewRow row in dgvlistaComprobantes.Rows)
                {
                    string cal = Convert.ToString(row.Cells[5].Value).Trim();
                    if (cal == "")
                    {
                        total1 += 1;
                    }
                }
                txt1.Text = total1.ToString();
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                decimal total2 = 0;
                txt2.Text = Convert.ToString(0);
                foreach (DataGridViewRow row in dgvlistaComprobantes.Rows)
                {
                    string cal2 = Convert.ToString(row.Cells[5].Value).Trim();
                    if (cal2 != "")
                    {
                        total2 += 1;
                    }
                }
                txt2.Text = total2.ToString();
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------
                decimal total3 = 0;
                txt3.Text = Convert.ToString(0);
                foreach (DataGridViewRow row in dgvlistaComprobantes.Rows)
                {
                    total3 += 1;
                }
                txt3.Text = total3.ToString();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE: " + ex.ToString();
            }

        }
        public static void GenerarYFirmarXML(string ruc, string tipo_doc, string serieCorrelaitvo, TextBox txt, string host)
        {
            try
            {
                WebRequest request = WebRequest.Create(host + "/api/GenerarComprobante.htm");
                request.Method = "POST";

                string postData = "{\"num_ruc\":\"" + ruc + "\",\"tip_docu\":\"" + tipo_doc + "\",\"num_docu\":\"" + serieCorrelaitvo + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
                response.Close();
            }
            catch (Exception ex)
            {
                txt.Text = "MENSAJE: " + ex;
            }
        }
        public static void EnviarXMLSunat(string ruc, string tipo_doc, string serieCorrelaitvo, TextBox txt,string host)
        {
            try
            {
                WebRequest request = WebRequest.Create(host+ "/api/enviarXML.htm");
                request.Method = "POST";

                txt.Text = "MENSAJE : INFORMANDO COMPROBANTE " + serieCorrelaitvo + " A SUNAT";
                string postData = "{\"num_ruc\":\"" + ruc + "\",\"tip_docu\":\"" + tipo_doc + "\",\"num_docu\":\"" + serieCorrelaitvo + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
                response.Close();
            }
            catch (Exception ex)
            {
                txt.Text = "MENSAJE : " + ex.ToString();
            }

        }
        public static DataTable GetDataTableFromJsonString(string json)
        {
            try
            {
                var jsonLinq = JObject.Parse(json);
                // Find the first array using Linq
                var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
                var trgArray = new JArray();
                foreach (JObject row in srcArray.Children<JObject>())
                {
                    var cleanRow = new JObject();
                    foreach (JProperty column in row.Properties())
                    {
                        // Only include JValue types
                        if (column.Value is JValue)
                        {
                            //ind_situ
                            //if (column.Name == "ind_situ")
                            //{
                            //    //selecct case
                            //    cleanRow.Add(column.Name, column.Value);
                            //}
                            //else
                            //{
                            //}

                            cleanRow.Add(column.Name, column.Value);
                        }

                    }
                    trgArray.Add(cleanRow);
                }
                return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
            }
            catch (Exception ex)
            {
                //txtmensaje.Text = "MENSAJE : " + ex.ToString();
                return JsonConvert.DeserializeObject<DataTable>(0.ToString());
            }

        }
        private void btngenyenviarM_Click(object sender, EventArgs e)
        {
            txtmensaje.Text = "MENSAJE : INICIAR GENERAR E INFORMAR A SUNAT MASIVO!";
            try
            {
                if (dgvlistaComprobantes.Rows.Count > 0)
                {
                    string ruc;
                    string tipodoc;
                    string seriecorrelativo;
                    foreach (DataGridViewRow fila in dgvlistaComprobantes.Rows)
                    {
                        ruc = Convert.ToString(fila.Cells[0].Value);
                        tipodoc = Convert.ToString(fila.Cells[1].Value);
                        seriecorrelativo = Convert.ToString(fila.Cells[2].Value);

                        GenerarYFirmarXML(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                        EnviarXMLSunat(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                    }
                }
                Listar_comprobantes(dgvlistaComprobantes, HostLink);
                Contar_Comprobantes();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE : " + ex.ToString();
            }
        }
        private void btngen_Click(object sender, EventArgs e)
        {
            try
            {
                string ruc = Convert.ToString(this.dgvlistaComprobantes.CurrentRow.Cells[0].Value);
                string tipodoc = Convert.ToString(this.dgvlistaComprobantes.CurrentRow.Cells[1].Value);
                string seriecorrelativo = Convert.ToString(this.dgvlistaComprobantes.CurrentRow.Cells[2].Value);



                //GenerarYFirmarXML(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                //Listar_comprobantes(dgvlistaComprobantes, HostLink);
                //Contar_Comprobantes();



                //txtmensaje.Text = "MENSAJE: GENERANDO XML " + seriecorrelativo;
            }
            catch (Exception ex)
            {
                //txtmensaje.Text = "MENSAJE : " + ex.ToString();
            }

        }



        private void btnenviar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruc = Convert.ToString(dgvlistaComprobantes.CurrentRow.Cells[0].Value);
                string tipodoc = Convert.ToString(dgvlistaComprobantes.CurrentRow.Cells[1].Value);
                string seriecorrelativo = Convert.ToString(dgvlistaComprobantes.CurrentRow.Cells[2].Value);

                EnviarXMLSunat(ruc, tipodoc, seriecorrelativo, txtmensaje, HostLink);
                Listar_comprobantes(dgvlistaComprobantes, HostLink);
                Contar_Comprobantes();
                //txtmensaje.Text = "MENSAJE: PROCESO DE ENVIANDO A SUNAT " + seriecorrelativo + " FINALIZADO.";
            }
            catch (Exception ex)
            {
                //txtmensaje.Text = "MENSAJE : " + ex.ToString();
            }

        }
        private void btnrutas_Click(object sender, EventArgs e)
        {
            if (estadodirect == "0")
            {
                estadodirect = "1";
            }
            else
            {
                estadodirect = "0";
            }
            if (estadodirect == "0")
            {
                txtorigen.Visible = false;
                txtdestino.Visible = false;
            }
            else
            {
                txtorigen.Visible = true;
                txtdestino.Visible = true;
            }
        }
        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow fila in dgvlistaComprobantes.Rows)
                {
                    string envio = Convert.ToString(fila.Cells[5].Value).Trim();
                    string obs = Convert.ToString(fila.Cells[6].Value).Trim();
                    if (envio != "" && obs.Trim() == "-")
                    {
                        string origen = txtorigen.Text + Convert.ToString(fila.Cells[7].Value).Trim() + ".CAB";
                        string destino = txtdestino.Text + Convert.ToString(fila.Cells[7].Value).Trim() + ".CAB";
                        mycomputer.FileSystem.MoveFile(origen, destino);

                        string origen1 = txtorigen.Text + Convert.ToString(fila.Cells[7].Value).Trim() + ".DET";
                        string destino1 = txtdestino.Text + Convert.ToString(fila.Cells[7].Value).Trim() + ".DET";
                        mycomputer.FileSystem.MoveFile(origen1, destino1);
                    }
                }

                LimpiarBandejaFact();
                Listar_comprobantes(dgvlistaComprobantes, HostLink);
                Contar_Comprobantes();
                txtmensaje.Text = "MENSAJE : PROCESO CULMINADO!";
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE : " + ex.ToString();
            }

        }
        public void LimpiarBandejaFact()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:9000/api/EliminarBandeja.htm");
                request.Method = "POST";

                string postData = "{\"rutaCertificado\":\\}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
                response.Close();
            }
            catch (Exception ex)
            {
                txtmensaje.Text = "MENSAJE : " + ex.ToString();
            }

        }

        private void lbltem2_TextChanged(object sender, EventArgs e)
        {
        }

        private void lbltem2_Click(object sender, EventArgs e)
        {

        }

        private void lbltemp_Click(object sender, EventArgs e)
        {
            if (est_temp == "1")
            {
                temporizador.Stop();
                est_temp = "0";
                lbltemp.ForeColor = Color.DarkCyan;
            }
            else
            {
                temporizador.Start();
                est_temp = "1";
                lbltemp.ForeColor = Color.Blue;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeneradorTXT txt = new GeneradorTXT();
            txt.Show();
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            sec1 = sec1 + 1;
            if (sec1 >= 10)   //REducir un segundo al contador
            {
                lbltemp.Text = Convert.ToString(Convert.ToInt32(lbltemp.Text) - 1);
                sec1 = 0;
            }

            if (lbltemp.Text == "10")
            {
                temporizador.Stop();
                Listar_comprobantes(dgvlistaComprobantes, HostLink);
                temporizador.Start();
            }

            if (lbltemp.Text == "0")
            {
                temporizador.Stop();
                btngenyenviarM.PerformClick();
                lbltemp.Text = "600";
            }
        }

        private void Temp2_Tick(object sender, EventArgs e)
        {
            try
            {
                sec2 = sec2 + 1;
                if (sec2 >= 20)   //Aumentar 2 segundo al contador
                {
                    lbltem2.Text = Convert.ToString(Convert.ToInt32(lbltem2.Text) + 2);
                    sec2 = 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        /************************************************************************************************************/
        /************************************************************************************************************/
        conexion cn = new conexion();

        private void comboBox1_ValueMemberChanged(object sender, EventArgs e)
        {
            int i = 0;
            string s = comboBox1.SelectedValue.ToString();
            if (int.TryParse(s, out i))
            {
                CargaDirectorio();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            string s = comboBox1.SelectedValue.ToString();
            if (int.TryParse(s, out i))
            {
                CargaDirectorio();
            }
        }

        SqlConnection scn = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();
        public void CargaDirectorio()
        {
            scn = cn.conectar();
            scn.Open();
            SqlCommand comand = new SqlCommand("select ruc, carpetaTrabajo, carpetaTxt, linkHost from Empresa where id = " + comboBox1.SelectedValue.ToString(), scn);

            SqlDataAdapter sda = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string V1 = dt.Rows[i][0].ToString();
                string V2 = dt.Rows[i][1].ToString();
                string V3 = dt.Rows[i][2].ToString();
                Rucc = V1;
                directorio = V2;
                directoriotxt = V3;
                HostLink = dt.Rows[i][3].ToString();
            }
        }
    }
}
