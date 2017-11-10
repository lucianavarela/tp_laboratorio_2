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

        public bool Guardar(List<string> datos)
        {
            try
            {
                FileStream file = new FileStream(this._ruta, FileMode.Create);
                BinaryFormatter file_serialized = new BinaryFormatter();
                file_serialized.Serialize(file, datos);
                file.Close();
            }
            catch (Exception)
            {
                throw new FileLoadException();
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
            try
            {
                if (File.Exists(this._ruta))
                {
                    BinaryFormatter file_deserialized;
                    FileStream file = new FileStream(this._ruta, FileMode.Open);
                    file_deserialized = new BinaryFormatter();
                    datos = (List<string>)file_deserialized.Deserialize(file);
                    file.Close();
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
