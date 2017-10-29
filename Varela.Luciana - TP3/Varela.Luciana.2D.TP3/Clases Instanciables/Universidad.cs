using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace ClasesInstanciadas
{
    public class Universidad
    {
        public enum EClases { Programacion, Laboratorio, Legislacion, SPD }

        List<Alumno> alumnos;
        List<Jornada> jornada;
        List<Profesor> profesores;

        public List<Alumno> Alumnos
        {
            get { return this.alumnos; }
            set { this.alumnos = value; }
        }

        public List<Jornada> Jornadas
        {
            get { return this.jornada; }
            set { this.jornada = value; }
        }

        public List<Profesor> Instructores
        {
            get { return this.profesores; }
            set { this.profesores = value; }
        }

        public Jornada this[int i]
        {
            get { return this.jornada[i]; }
            set { this.jornada[i] = value; }
        }

        public Universidad()
        {
            this.Alumnos = new List<Alumno>();
            this.Instructores = new List<Profesor>();
            this.Jornadas = new List<Jornada>();
        }

        public static bool Guardar(Universidad gim)
        {
            string ruta = "../../../Universidad.xml";
            Xml<Universidad> archivo = new Xml<Universidad>();
            bool respuesta = archivo.Guardar(ruta, gim);
            return respuesta;
        }

        public static Universidad Leer()
        {
            Universidad universidad_leida = new Universidad();
            string ruta = "../../../Universidad.xml";
            Xml<Universidad> archivo = new Xml<Universidad>();

            if (archivo.Leer(ruta, out universidad_leida))
            {
                return universidad_leida;
            }
            else
            {
                return null;
            }
        }

        static string MostrarDatos(Universidad gim)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("*- UNIVERSIDAD -*");
            sb.AppendLine("");
            sb.AppendLine("JORNADA:");
            foreach (Jornada j in gim.Jornadas)
            {
                sb.Append(j.ToString());
                sb.AppendLine("**********");
            }
            sb.Append("<------------------------------------------------>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return MostrarDatos(this);
        }

        public static bool operator ==(Universidad g, Alumno a)
        {
            if (g.Alumnos.Count > 0)
            {
                foreach (Alumno alumno in g.Alumnos)
                {
                    if (alumno == a)
                        return true;
                }
            }
            return false;
        }

        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        public static Profesor operator ==(Universidad g, Universidad.EClases clase)
        {
            foreach (Profesor i in g.Instructores)
            {
                if (i == clase)
                {
                    return i;
                }
            }
            string mensaje = "No hay Profesor para la clase.";
            throw new SinProfesorException(mensaje);
        }

        public static Profesor operator !=(Universidad g, Universidad.EClases clase)
        {
            Profesor p = null;
            foreach (Profesor i in g.Instructores)
            {
                if (i != clase)
                {
                    p = i;
                    break;
                }
            }
            return p;
        }

        public static bool operator ==(Universidad g, Profesor i)
        {
            if (g.Instructores.Count > 0)
            {
                foreach (Profesor profesor in g.Instructores)
                {
                    if (profesor == i)
                        return true;
                }
            }
            return false;
        }

        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        public static Universidad operator +(Universidad g, Alumno a)
        {
            if (g != a)
            {
                g.Alumnos.Add(a);
            }
            else
            {
                string mensaje = "Alumno repetido.";
                throw new AlumnoRepetidoException(mensaje);
            }
            return g;
        }

        public static Universidad operator +(Universidad g, Universidad.EClases clase)
        {
            Profesor p = (g == clase);
            List<Alumno> alumnos = new List<Alumno>();

            foreach (Alumno a in g.Alumnos)
            {
                if (a == clase)
                {
                    alumnos.Add(a);
                }
            }
            Jornada nueva_jornada = new Jornada(clase, p);
            nueva_jornada.Alumnos = alumnos;
            g.Jornadas.Add(nueva_jornada);

            return g;
        }

        public static Universidad operator +(Universidad g, Profesor i)
        {
            if (g != i)
            {
                g.Instructores.Add(i);
            }
            return g;
        }
    }
}
