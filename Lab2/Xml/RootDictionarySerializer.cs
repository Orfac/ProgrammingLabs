using Lab2.Models;
using System.Xml;
using System.Xml.Serialization;

namespace Lab2.Xml
{
    /// <summary>
    /// Used for serializing and deserializing root groups 
    /// and their root in RootDicitonary
    /// </summary>
    public class RootGroupAndRoot
    {
        /// <summary>
        /// root of RootGroup
        /// </summary>
        public string root;
        /// <summary>
        /// RootGroup
        /// </summary>
        public RootGroup rootGroup;

        /// <summary>
        /// Empty constructor which does nothing
        /// </summary>
        public RootGroupAndRoot()
        {
        }

        /// <summary>
        /// Sets params
        /// </summary>
        /// <param name="root">Root for set</param>
        /// <param name="rootGroup">RootGroup for set</param>
        public RootGroupAndRoot(string root, RootGroup rootGroup)
        {
            this.root = root;
            this.rootGroup = rootGroup;
        }

    }
    /// <summary>
    /// Serializes and deserializes RootDictionary from or to xml
    /// </summary>
    public static class RootDictionarySerializer
    {
        /// <summary>
        /// Serializes RootDictionary to file by XmlWriter
        /// </summary>
        /// <param name="writer">writer from XmlWriter stream</param>
        /// <param name="rootDictionary"> RootDictionary for writing </param>
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

        /// <summary>
        /// Deserializes RootDictionary from file by XmlReader
        /// </summary>
        /// <param name="reader">reader from XmlReader stream</param>
        /// <param name="rootDictionary"> RootDictionary for reading </param>
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
