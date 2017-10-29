using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        public bool Guardar (string archivo, string datos)
        {
            StreamWriter nuevo_archivo = new StreamWriter(archivo);
            nuevo_archivo.Write(datos);
            nuevo_archivo.Close();

            if (File.Exists(archivo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Leer (string archivo, out string datos)
        {
            if (File.Exists(archivo))
            {
                StreamReader archivo_leido = new StreamReader(archivo);
                datos = archivo_leido.ReadToEnd();
                return true;
            }
            else
            {
                datos = null;
                return false;
            }
        }
    }
}
