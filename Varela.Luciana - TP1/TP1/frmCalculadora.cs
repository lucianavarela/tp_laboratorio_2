using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    public partial class frmCalculadora : Form
    {
        public Calculadora miCalculadora = new Calculadora();

        public frmCalculadora()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// Metodo que trabaja cuando el boton '=' es presionado, y realiza la operacion deseada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperar_Click(object sender, EventArgs e)
        {
            Numero numero1 = new Numero(txtNumero1.Text);
            Numero numero2 = new Numero(txtNumero2.Text);
            string operador = cmbOperacion.Text;
            string operadorValidado = miCalculadora.validarOperador(operador);
            cmbOperacion.Text = operadorValidado;
            Numero resultado = new Numero(miCalculadora.operar(numero1, numero2, operadorValidado));
            string resultadoString = (resultado.getNumero()).ToString();
            lblResultado.Text = resultadoString;
        }

        /// <summary>
        /// Metodo que limpia los valores de la calculadora.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            lblResultado.Text = "";
            cmbOperacion.Text = "";
        }
    }
}
