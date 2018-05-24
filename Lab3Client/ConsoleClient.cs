using DictionaryLib;
using DictionaryLib.Models;
using DictionaryLib.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Lab3Client
{
    public class ConsoleClient
    {
        private string host;
        private int port;
        private Encoding encoding;
        private Client _client;

        public ConsoleClient(string host, int port, Encoding encoding)
        {
            this.host = host;
            this.port = port;
            this.encoding = encoding;
            _client = new Client(host, port, Encoding.UTF8);
        }

        public void Connect()
        {
            _client.Connect();
        }

        public void Run()
        {
            try
            {
                _client.Connect();
            }
            catch (SocketException)
            {
                Console.WriteLine("Не получилось связаться с сервером");
            }
            string value;
            Console.WriteLine("Вводите слова для поиска или добавления в словарь.\nВведите q для выхода.");
            Console.Write(">");
            while ((value = Console.ReadLine()) != "q")
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    Console.Write(">");
                    continue;
                }
                try
                {
                    if (_client.Connected)
                    {
                        HandleWord(value);
                    }
                    else
                    {
                        Console.WriteLine("Пытаемся восстановить соединение");
                        _client.Connect();
                        if (_client.Connected)
                        {
                            Console.WriteLine("Соединение восстановлено");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось восстановить соединение");
                        }
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Не получилось связаться с сервером");
                    continue;
                }
                finally
                {
                    Console.Write(">");
                }
            }
        }

        private void HandleWord(string value)
        {
            Response response = _client.SeekWord(value);
            switch (response.StatusCode)
            {
                case StatusCode.OK:
                    PrintWords(response.Body);
                    break;
                case StatusCode.ObjectNotFound:
                    AskForAdd(value);
                    break;
                default:
                    PrintError();
                    break;
            }
        }

        private void PrintError()
        {
            Console.WriteLine("Произошла ошибка, повторите операцию");
        }

        private void AskForAdd(string value)
        {
            Console.Write("Такого слова не существует, хотите добавить в словарь?y/n:");
            string answer = Console.ReadLine();
            if (answer.ToLower() != "y")
            {
                return;
            }
            Word newWord = WordParser.ConsoleParse(value);
            HandleNewWord(newWord);
        }

        private void HandleNewWord(Word newWord)
        {
            Response response = _client.AddWord(newWord);
            switch (response.StatusCode)
            {
                case StatusCode.Created:
                    Console.WriteLine("Слово успешно добавлено в словарь");
                    break;
                case StatusCode.Сonflict:
                    Console.WriteLine("Слово уже существует");
                    break;
                default:
                    PrintError();
                    break;
            }
        }

        private void PrintWords(string words)
        {
            if (words == null)
            {
                PrintError();
                return;
            }
            Console.WriteLine(words);
        }
    }
}
