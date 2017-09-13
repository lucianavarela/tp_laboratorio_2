using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public class Calculadora
    {
        /// <summary>
        /// Metodo encargado de realizar la operacion matematica deseada y devolver el resultado.
        /// </summary>
        /// <param name="numero1"></param>
        /// <param name="numero2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        public double operar(Numero numero1, Numero numero2, string operador) {
            double numeroUno = numero1.getNumero();
            double numeroDos = numero2.getNumero();
            switch (operador) {
                case "+":
                    return (numeroUno + numeroDos);
                case "-":
                    return (numeroUno - numeroDos);
                case "*":
                    return (numeroUno * numeroDos);
                case "/":
                    if (numeroDos == 0) {
                        return 0;
                    } else {
                        return (numeroUno / numeroDos);
                    }
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Metodo encargado de validar el operador ingresado a la calculadora.
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns>
        public string validarOperador (string operador) {
            switch (operador) {
                case "+":
                    return "+";
                case "-":
                    return "-";
                case "*":
                    return "*";
                case "/":
                    return "/";
                default:
                    return "+";
            }
        }
    }
}
