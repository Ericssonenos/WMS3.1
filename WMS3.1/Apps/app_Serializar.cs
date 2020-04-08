
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WMS3._1.Models;
using System.IO;

namespace WMS3._1
{
    class app_Serializar
    {
        public T serializarobjeto<T>(Stream arquivo) where T : class
        {
            var serialize = new XmlSerializer(typeof(T));

            try
            {
                var xmlArquivo = XmlReader.Create(arquivo);
                return (T)serialize.Deserialize(xmlArquivo);
            }
            catch (Exception)
            {
                return null;
            }
        }

      

    }
}