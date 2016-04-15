using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntityFac
{
    public class SerializeHelper
    {
        public static string GetPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + "config.xml";
        }
        public static void Serialize<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, obj, ns);
                using (StreamWriter file = new StreamWriter(GetPath()))
                {
                    file.Write(sw.ToString());
                }
            }

        }

        public static T Deserialize<T>()
        {
            using (StreamReader file = new StreamReader(GetPath()))
            {
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(T));
                return (T)xmlSearializer.Deserialize(file);
            }
        }
    }
    [Serializable]
    public class ConfigInfo
    {
        public string ServerAddress { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string DataBase { get; set; }
        public string NameSpace { get; set; }
        public string Prefix { get; set; }
        public string FilePath { get; set; }
        public string ConnectionString { get; set; }
    }
}
