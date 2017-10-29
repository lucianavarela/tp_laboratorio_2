using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace ClasesInstanciadas
{
    public sealed class Alumno : Universitario
    {
        public enum EEstadoCuenta {AlDia, Deudor, Becado}

        Universidad.EClases _claseQueToma;
        EEstadoCuenta _estadoCuenta;

        public Alumno() : base() { }

        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._claseQueToma = claseQueToma;
        }

        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._claseQueToma = claseQueToma;
            this._estadoCuenta = estadoCuenta;
        }

        protected override string MostrarDatos()
        {
            string cuenta = this._estadoCuenta.ToString();
            if (this._estadoCuenta == EEstadoCuenta.AlDia) cuenta = "Cuota al dia";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.MostrarDatos());
            sb.AppendFormat("{0}",this.ParticiparEnClase());
            sb.AppendLine();
            sb.AppendFormat("Estado de Cuenta: {0}", cuenta);
            return sb.ToString();
        }

        protected override string ParticiparEnClase()
        {
            return "TOMA CLASE DE " + this._claseQueToma;
        }

        public override string ToString()
        {
            return this.MostrarDatos();
        }

        public static bool operator == (Alumno a, Universidad.EClases clase)
        {
            bool respuesta = false;
            if (clase == a._claseQueToma && a._estadoCuenta != EEstadoCuenta.Deudor)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public static bool operator !=(Alumno a, Universidad.EClases clase)
        {
            return !(a == clase);
        }
    }
}
