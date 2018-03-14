using Lab2.Models;
using System;
using System.IO;

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
                "Считать из .xml файла?" +
                "\nВведите символ д для считывания, любой другой для пропуска"
                 );
            if (IsEndOfDialog())
            {
                Console.WriteLine();
                Console.WriteLine("Считывание отменено");
                return;
            }
            Console.WriteLine();
            string path = GetPathForRead();
            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine("Считывание отменено");
                return;
            }

            Input.ReadRootDictionaryFromXml(path,rootDictionary);
            
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
                Console.WriteLine();
                Console.WriteLine("Сохранение отменено");
                return;
            }
            Console.WriteLine();
            string path = GetPathForWrite();
            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine("Сохранение отменено");
                return;
            }

            Output.WriteRootDictionaryToXml(path, rootDictionary);

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
