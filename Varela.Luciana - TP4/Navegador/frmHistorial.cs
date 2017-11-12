using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegador
{
    public delegate void ManejoURL(string url);
    public partial class frmHistorial : Form
    {
        List<string> _urls;
        public const string ARCHIVO_HISTORIAL = "historico.dat";
        Archivos.Texto archivos = new Archivos.Texto(ARCHIVO_HISTORIAL);

        public frmHistorial()
        {
            InitializeComponent();
        }

        private void frmHistorial_Load(object sender, EventArgs e)
        {
            lstHistorial.Items.Clear();
            if (archivos.Leer(out _urls))
            {
                if (this._urls.Count > 0)
                {
                    foreach (string url in this._urls)
                    {
                        lstHistorial.Items.Add(url);
                    }
                }
            }
        }

        private void lstHistorial_DoubleClick(object sender, EventArgs e)
        {
            string url = (string)lstHistorial.SelectedItem;
            EventoURLSeleccionada.Invoke(url);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public event ManejoURL EventoURLSeleccionada;

        private void frmHistorial_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }
    }
}
