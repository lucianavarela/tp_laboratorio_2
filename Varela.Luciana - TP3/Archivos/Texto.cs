using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excepciones;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        public bool Guardar(string archivo, string datos)
        {
            try
            {
                StreamWriter nuevo_archivo = new StreamWriter(archivo);
                nuevo_archivo.Write(datos);
                nuevo_archivo.Close();
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }


            if (File.Exists(archivo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Leer(string archivo, out string datos)
        {
            try
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
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
        }
    }
}
