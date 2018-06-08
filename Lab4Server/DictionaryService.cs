using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using DictionaryLib;
using DictionaryLib.Models;
using DictionaryLib.Services;

namespace Lab4Server
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.Single
  )]
    public class DictionaryService : IDictionaryContract
    {
        private RootDictionary _rootDictionary;
        private Object thisLock = new Object();

        public DictionaryService()
        {
            this._rootDictionary = new RootDictionary();
        }

        public string AddWord(Word word)
        {
            if (_rootDictionary.Contains(word.Value))
            {
                return "Слово уже существует";
            }
            
            lock (thisLock)
            {
                if (_rootDictionary.Contains(word.Value))
                    return "Слово уже существует";

                if (WordParser.IsComplexWordValid(word))
                {
                    _rootDictionary.Add(word);
                    return "Слово успешно добавлено";
                }
            }
            return "Произошла ошибка, повторите операцию";
        }

        public string FindWord(string word)
        {
            if (_rootDictionary.Contains(word))
            {
                string root = _rootDictionary.GetRoot(word);
                List<Word> cognateWords = _rootDictionary.RootGroups[root].Words;
                var wordsToReturn = new StringBuilder();
                for (int i = 0; i < cognateWords.Count; i++)
                {
                    wordsToReturn.Append(cognateWords[i]);
                    wordsToReturn.Append('\n');
                }
                return wordsToReturn.ToString();
            }
            else
            {
                return "Такого слова не существует";
            }
        }
    }
}