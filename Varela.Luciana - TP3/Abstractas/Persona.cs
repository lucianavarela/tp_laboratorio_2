using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace EntidadesAbstractas
{
    public abstract class Persona
    {
        public enum ENacionalidad { Argentino, Extranjero }

        string _apellido;
        string _nombre;
        int _dni;
        ENacionalidad _nacionalidad;

        public string Apellido
        {
            get { return this._apellido; }
            set
            {
                string apellido = ValidarNombreApellido(value);
                if (apellido != null)
                {
                    this._apellido = apellido;
                }
            }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set
            {
                string nombre = ValidarNombreApellido(value);
                if (nombre != null)
                {
                    this._nombre = nombre;
                }
            }
        }

        public int DNI
        {
            get { return this._dni; }
            set
            {
                this._dni = ValidarDni(this.Nacionalidad, value);
            }
        }

        public ENacionalidad Nacionalidad
        {
            get { return this._nacionalidad; }
            set { this._nacionalidad = value; }
        }

        public string StringToDNI
        {
            set
            {
                this._dni = ValidarDni(this.Nacionalidad, value);
            }
        }

        public Persona()
        {
            this.Apellido = "";
            this.Nombre = "";
        }

        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
        }

        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad)
        {
            this.Nacionalidad = nacionalidad;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.DNI = dni;
        }

        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
        {
            this.Nacionalidad = nacionalidad;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.StringToDNI = dni;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Apellido + ", " + this.Nombre);
            sb.AppendLine("DNI: " + this.DNI);
            sb.AppendLine("Nacionalidad: " + this.Nacionalidad);
            return sb.ToString();
        }

        int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (ENacionalidad.Argentino == nacionalidad && (dato < 1 || dato > 89999999))
            {
                string mensaje = "La nacionalidad no se condice con el numero de DNI";
                throw new DniInvalidoException(mensaje);
            }
            else if (ENacionalidad.Extranjero == nacionalidad && dato < 89999999)
            {
                string mensaje = "La nacionalidad no se condice con el numero de DNI";
                throw new NacionalidadInvalidaException(mensaje);
            }
            else
            {
                return dato;
            }
        }

        int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int dni;
            if (int.TryParse(dato, out dni))
            {
                dni = ValidarDni(this.Nacionalidad, dni);
            }
            return dni;
        }

        string ValidarNombreApellido(string dato)
        {
            bool invalid = false;
            foreach (char i in dato)
            {
                if (char.IsNumber(i))
                {
                    invalid = true;
                    break;
                }
            }
            if (!(invalid))
            {
                return dato;
            }
            else
            {
                return null;
            }
        }
    }
}
