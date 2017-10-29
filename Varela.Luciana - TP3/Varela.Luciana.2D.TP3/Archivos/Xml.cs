using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Excepciones;

namespace Archivos
{
    public class Xml<T> : IArchivo<T>
    {
        public bool Guardar(string archivo, T datos)
        {
            try
            {
                StreamWriter archivo_escrito = new StreamWriter(archivo);
                XmlSerializer contenido_serializado = new XmlSerializer(typeof(T));
                contenido_serializado.Serialize(archivo_escrito, datos);
                archivo_escrito.Close();
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

        public bool Leer(string archivo, out T datos)
        {
            try
            {
                if (File.Exists(archivo))
                {
                    XmlTextReader archivo_leido = new XmlTextReader(archivo);
                    XmlSerializer contenido_deserializado = new XmlSerializer(typeof(T));
                    datos = (T)contenido_deserializado.Deserialize(archivo_leido);
                    archivo_leido.Close();

                    return true;
                }
                else
                {
                    datos = default(T);
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
