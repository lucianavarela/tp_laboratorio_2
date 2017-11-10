using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Hilo;

namespace Navegador
{

    public partial class frmWebBrowser : Form
    {
        List<string> _urls;
        private const string ESCRIBA_AQUI = "Escriba aquí...";
        Archivos.Texto archivos;

        public frmWebBrowser()
        {
            InitializeComponent();
            this._urls = new List<string>();
        }

        private void frmWebBrowser_Load(object sender, EventArgs e)
        {
            this.txtUrl.SelectionStart = 0;  //This keeps the text
            this.txtUrl.SelectionLength = 0; //from being highlighted
            this.txtUrl.ForeColor = Color.Gray;
            this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;

            archivos = new Archivos.Texto(frmHistorial.ARCHIVO_HISTORIAL);
            if (!(archivos.Leer(out this._urls)))
            {

            }
        }

        #region "Escriba aquí..."
        private void txtUrl_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam; //Without this the mouse pointer shows busy
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(frmWebBrowser.ESCRIBA_AQUI) == true)
            {
                this.txtUrl.Text = "";
                this.txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(null) == true || this.txtUrl.Text.Equals("") == true)
            {
                this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;
                this.txtUrl.ForeColor = Color.Gray;
            }
        }

        private void txtUrl_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtUrl.SelectAll();
        }
        #endregion

        public delegate void ProgresoDescargaCallback(int progreso);
        private void ProgresoDescarga(int progreso)
        {
            if (statusStrip.InvokeRequired)
            {
                ProgresoDescargaCallback d = new ProgresoDescargaCallback(ProgresoDescarga);
                this.Invoke(d, new object[] { progreso });
            }
            else
            {
                tspbProgreso.Value = progreso;
                if (progreso == 100)
                {
                    tspbProgreso.Value = 0;
                }
            }
        }

        delegate void FinDescargaCallback(string html);
        private void FinDescarga(string html)
        {
            if (rtxtHtmlCode.InvokeRequired)
            {
                FinDescargaCallback d = new FinDescargaCallback(FinDescarga);
                this.Invoke(d, new object[] { html });
            }
            else
            {
                if (html == "404")
                {
                    MessageBox.Show("404 - File not found :(");
                }
                else
                {
                    rtxtHtmlCode.Text = html;
                }
            }
        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            bool add_to_history = true;
            string link = txtUrl.Text;
            if (!(link.Contains("http")))
            {
                link = "https://" + link;
            }
            if ((this._urls).Count > 0)
            {
                foreach (string historyURL in this._urls)
                {
                    if (historyURL == link)
                    {
                        add_to_history = false;
                        break;
                    }
                }
            }
            if (add_to_history)
            {
                this._urls.Add(link);
            }
            
            Uri url = new Uri(link);
            Descargador busqueda = new Descargador(url);

            busqueda.EventoProgress += ProgresoDescarga;
            busqueda.EventoComplete += FinDescarga;
            Thread hiloDescarga = new Thread(busqueda.IniciarDescarga);
            hiloDescarga.Start();
        }
        
        private void mostrarTodoElHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorial formHistorial = new frmHistorial(this._urls);
            formHistorial.EventoURLSeleccionada += URLdeHistorial;
            formHistorial.Show();
        }

        private void URLdeHistorial (string url)
        {
            txtUrl.Text = url;
            btnIr.PerformClick();
        }

        private void frmWebBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            archivos.Guardar(this._urls);
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnIr.PerformClick();
            }
        }

    }
}
