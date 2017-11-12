using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        string _ruta;

        public Texto(string archivo)
        {
            this._ruta = archivo;
        }

        public bool Guardar(string datos)
        {
            try
            {
                StreamWriter writer = File.AppendText(this._ruta);
                writer.WriteLine(datos);
                writer.Close();
            }
            catch (Exception)
            {
                return false;
            }

            if (File.Exists(this._ruta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Leer(out List<string> datos)
        {
            datos = new List<string>();
            try
            {
                if (File.Exists(this._ruta))
                {
                    StreamReader reader = new StreamReader(this._ruta);
                    while (!(reader.EndOfStream))
                    {
                        datos.Add((string)reader.ReadLine());
                    }
                    reader.Close();
                    return true;
                }
                else
                {
                    datos = new List<string>();
                    return false;
                }
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }
        }
    }
}
