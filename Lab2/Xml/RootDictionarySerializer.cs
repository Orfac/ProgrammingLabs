using Lab2.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lab2.Xml
{
    public class RootGroupAndRoot
    {
        
        public string root;
        public RootGroup rootGroup;

        public RootGroupAndRoot()
        {
        }

        public RootGroupAndRoot(string root, RootGroup rootGroup)
        {
            this.root = root;
            this.rootGroup = rootGroup;
        }

    }

    public static class RootDictionarySerializer
    {
        public static void Serialize(XmlWriter writer, RootDictionary rootDictionary)
        {
            XmlSerializer itemSerialize = new XmlSerializer(typeof(RootGroupAndRoot));
            writer.WriteStartElement("RootDictionary");
            foreach (string key in rootDictionary.RootGroups.Keys)
            {
                var item = new RootGroupAndRoot(key, rootDictionary.RootGroups[key]);

                writer.WriteStartElement("RootGroupAndRoot");
                itemSerialize.Serialize(writer, item);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();      
        }

        public static void Deserialize(XmlReader reader, RootDictionary rootDictionary)
        {
            XmlSerializer itemSerialize = new XmlSerializer(typeof(RootGroupAndRoot));

            reader.ReadStartElement("RootDictionary");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("RootGroupAndRoot");
                RootGroupAndRoot item = (RootGroupAndRoot)itemSerialize.Deserialize(reader);
                reader.ReadEndElement();

                rootDictionary.RootGroups.Add(item.root, item.rootGroup);
            }

            reader.ReadEndElement();
        }
    }
}
