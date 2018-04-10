using Lab2.Models;
using global::System;
using System.IO;
using System.Xml;


namespace Lab2.Xml
{
    public class XmlConsoleDialoger
    {
        /// <summary>
        /// Starts console dialog for reading or not reading rootdictionary from .xml file
        /// </summary>
        /// <param name="rootDictionary">where should be written RootDictionary from .xml file</param>
        public void StartReadDialog(RootDictionary rootDictionary)
        {
            Console.WriteLine(
                "Считать из .xml файла?\n" +
                "Введите символ д для считывания, любой другой для пропуска"
                 );
            if (IsEndOfDialog())
            {
                PrintCanselMessage("Считывание");
                return;
            }

            Console.WriteLine();
            string path = GetPathForRead();

            if (String.IsNullOrEmpty(path))
            {
                PrintCanselMessage("Считывание");
                return;
            }

            PrintStartMessage("Считывание");
            ReadRootDictionaryFromXml(path,rootDictionary);
            PrintEndMessage("Считывание");
        }

        /// <summary>
        /// Reads RootDictionary from .xml file
        /// </summary>
        /// <param name="path"> path to file</param>
        /// <returns>readRootDictionary</returns>
        private static void ReadRootDictionaryFromXml(string path, RootDictionary rootDictionary)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                var settings = new XmlReaderSettings
                {
                    IgnoreWhitespace = true
                };
                using (XmlReader reader = XmlReader.Create(fileStream, settings))
                {
                    RootDictionarySerializer.Deserialize(reader, rootDictionary);
                }
            }
        }

        /// <summary>
        /// Starts console dialog for writing or not writing rootdictionary to .xml file
        /// </summary>
        /// <param name="rootDictionary">what should be written to .xml file</param>
        public void StartWriteDialog(RootDictionary rootDictionary)
        {
            Console.WriteLine(
                "Записать в .xml файл?" +
                "\nВведите символ д для записи, любой другой для пропуска"
                    );

            if (IsEndOfDialog())
            {
                PrintCanselMessage("Сохранение");
                return;
            }

            Console.WriteLine();
            string path = GetPathForWrite();
            if (String.IsNullOrEmpty(path))
            {
                PrintCanselMessage("Сохранение");
                return;
            }

            PrintStartMessage("Сохранение");
            WriteRootDictionaryToXml(path, rootDictionary);
            PrintEndMessage("Сохранение");
        }

        /// <summary>
        /// Writes RootDictionary to file
        /// </summary>
        /// <param name="path">file where to write</param>
        /// <param name="rootDictionary">RootDictionary for writing</param>
        private static void WriteRootDictionaryToXml(string path, RootDictionary rootDictionary)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true
                };
                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    RootDictionarySerializer.Serialize(writer, rootDictionary);
                }
            }
            
        }

        private void PrintCanselMessage(string actionName)
        {
            Console.WriteLine("\n" + actionName + " отменено");
        }

        private void PrintStartMessage(string actionName)
        {
            Console.WriteLine(actionName + " начинается");
        }

        private void PrintEndMessage(string actionName)
        {
            Console.WriteLine(actionName + " завершено");
        }

        private char GetKey()
        {
            return Console.ReadKey().KeyChar;
        }

        private bool IsEndOfDialog()
        {
            if (GetKey() == 'д')
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private string GetPathForRead()
        {
            string path;
            do
            {
                Console.Write("Введите путь к файлу для чтения" +
                " или пустую строчку для отмены:");
                path = Console.ReadLine();
            } while (!(IsPathValidOrNull(path)));
            return path;

        }

        private bool IsPathValidOrNull(string path)
        {
            return String.IsNullOrEmpty(path) || File.Exists(path);
        }

        private string GetPathForWrite()
        {
            string path;
            do
            {
                Console.Write("Введите путь к файлу для записи " +
                    "или пустую строчку для отмены:");
                path = Console.ReadLine();
            } while (!IsPathExtensionValidOrNull(path));
            return path;
        }

        private bool IsPathExtensionValidOrNull(string path)
        {
            return String.IsNullOrEmpty(path) || (Path.GetExtension(path) == ".xml");
        }
    }
}
