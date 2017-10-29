using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace ClasesInstanciadas
{
    public class Jornada
    {
        List<Alumno> _alumnos;
        Universidad.EClases _clase;
        Profesor _instructor;

        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { this._alumnos = value; }
        }

        public Universidad.EClases Clase
        {
            get { return this._clase; }
            set { this._clase = value; }
        }

        public Profesor Instructor
        {
            get { return this._instructor; }
            set { this._instructor = value; }
        }

        Jornada()
        {
            this.Alumnos = new List<Alumno>();
        }

        public Jornada(Universidad.EClases clase, Profesor instructor) : this()
        {
            this.Clase = clase;
            this.Instructor = instructor;
        }

        public static bool Guardar(Jornada jornada)
        {
            string ruta = "../../../Jornada.txt";
            Texto archivo = new Texto();
            bool respuesta = archivo.Guardar(ruta, jornada.ToString());
            return respuesta;
        }

        public static string Leer()
        {
            string jornada_leida;
            string ruta = "../../../Jornada.txt";
            Texto archivo = new Texto();

            if (archivo.Leer(ruta, out jornada_leida))
            {
                return jornada_leida;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CLASE DE {0} DICTADA POR {1}", this.Clase.ToString(), this.Instructor.ToString());
            if (this.Alumnos.Count == 0)
            {
                sb.AppendLine("Esta clase no tiene alumnos.");
            }
            else
            {
                sb.AppendLine("Alumnos:");
                foreach (Alumno alumno in this.Alumnos)
                {
                    sb.AppendLine(alumno.ToString());
                    sb.AppendLine("---");
                }
            }

            return sb.ToString();
        }

        public static bool operator ==(Jornada j, Alumno a)
        {
            if (j.Alumnos.Count > 0)
            {
                foreach (Alumno alumno in j.Alumnos)
                {
                    if (alumno == a)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }

        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (j != a && a != j.Instructor)
            {
                j.Alumnos.Add(a);
                return j;
            }
            else
            {
                string mensaje = "Alumno repetido.";
                throw new AlumnoRepetidoException(mensaje);
            }
        }
    }
}
