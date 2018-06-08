using DictionaryLib;
using DictionaryLib.Models;
using System;
using System.Threading;
using DictionaryLib.Services;

namespace Lab4Client
{
    public class ConsoleClient
    {
        private Thread animation;
        private bool animationEnd;
        private IDictionaryContract channel;

        public ConsoleClient(IDictionaryContract channel)
        {
            this.channel = channel;
        }

        public void Run()
        {
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

                HandleWord(word);
                Console.Write(">");
            }
        }

        private void HandleWord(string value)
        {
            animationEnd = false;
            animation = new Thread(LoadingAnimation);
            animation.Start();
            Thread.Sleep(100);
            
            string response = channel.FindWord(value);
            animationEnd = true;
            Console.Write('\n');
            Console.WriteLine(response);

            if (response == "Такого слова не существует")
            {
                AskForAdd(value);
            }
        }

        private void AskForAdd(string value)
        {
            Console.Write("Хотите добавить в словарь?y/n:");
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
            string response = channel.AddWord(newWord);
            animationEnd = true;
            Console.Write('\n');
            Console.WriteLine(response);
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
