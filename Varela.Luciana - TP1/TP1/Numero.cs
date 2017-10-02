using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Numero
    {
        private double _numero;

        /// <summary>
        /// Constructor de un Numero en 0.
        /// </summary>
        public Numero() {
            this._numero = 0;
        }

        /// <summary>
        /// Constructor de un Numero a partir del double ingresado.
        /// </summary>
        /// <param name="numero"></param>
        public Numero(double numero) {
            this._numero = numero;
        }

        /// <summary>
        /// Constructor de un Numero a partir del string ingresado.
        /// </summary>
        /// <param name="numero"></param>
        public Numero(string numero) {
            this.setNumero(numero);
        }

        /// <summary>
        /// Metodo que devuelve el numero de la instancia de Numero
        /// </summary>
        /// <returns></returns>
        public double getNumero() {
            return this._numero;
        }

        /// <summary>
        /// Metodo que setea el numero de la instancia de Numero a partir del string ingresado.
        /// </summary>
        /// <param name="numero"></param>
        private void setNumero(string numero) {
            double numberDouble = validarNumero(numero);
            this._numero = numberDouble;
        }

        /// <summary>
        /// Metodo que valida que el string ingresado como parametro tenga el formato correcto para ser numero de una instancia de Numero.
        /// </summary>
        /// <param name="numeroString"></param>
        /// <returns></returns>
        private static double validarNumero(string numeroString) {
            double numero;
            if (double.TryParse(numeroString, out numero)) {
                return numero;
            } else {
                return 0;
            }
        }
    }
}
