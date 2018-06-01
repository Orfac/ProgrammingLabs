using DictionaryLib;
using DictionaryLib.Models;
using DictionaryLib.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab3Client
{
    public class ConsoleClient
    {
        private string host;
        private int port;
        private Encoding encoding;
        private Client _client;
        private Thread animation;
        private bool animationEnd;

        public ConsoleClient(string host, int port, Encoding encoding)
        {
            this.host = host;
            this.port = port;
            this.encoding = encoding;
            _client = new Client(host, port, Encoding.UTF8);
            
        }

        private bool ConnectionDialog()
        {
            bool isDialogEnd = false;
            do
            {
                Console.WriteLine("Не получилось связаться с сервером.");
                Console.WriteLine("Нажмите 'y' для переподключения, другую клавишу для завершения сеанса ");
                if (GetKey() == 'y')
                {
                    try
                    {
                        _client.Connect();
                    }
                    catch (InvalidOperationException)
                    {
                        isDialogEnd = true;
                        Console.WriteLine("Не удалось восстановить соединение");
                    }   
                }
                else
                {
                    isDialogEnd = true;
                }
                Console.WriteLine();

            } while ((!isDialogEnd) && (!_client.Connected));
            return _client.Connected;
        }

        private char GetKey()
        {
            return Console.ReadKey().KeyChar;
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
            string word = null;
            Console.WriteLine("Вводите слова для поиска или добавления в словарь.\nВведите q для выхода.");
            Console.Write(">");
            bool isProgrammEnd = false;
            
            while ((!isProgrammEnd) && (word = Console.ReadLine()) != "q")
            {
                if (String.IsNullOrWhiteSpace(word))
                {
                    Console.Write(">");
                    continue;
                }
                try
                {
                    if (_client.Connected)
                    {
                        HandleWord(word);
                    }
                    else
                    {
                        isProgrammEnd = !ConnectionDialog();
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
            animationEnd = false;
            animation = new Thread(LoadingAnimation);
            animation.Start();
            Thread.Sleep(100);
            Response response = _client.SeekWord(value);
            animationEnd = true;

            Console.Write('\n');
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
            animationEnd = false;
            animation = new Thread(LoadingAnimation);
            animation.Start();
            Thread.Sleep(100);
            Response response = _client.AddWord(newWord);
            animationEnd = true;
            Console.Write('\n');
            
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
            Console.Write(words);
        }

        private void LoadingAnimation()
        {
            while (!animationEnd)
            {
                Console.Write('.');
                Thread.Sleep(50);
            }
        }
    }
}
