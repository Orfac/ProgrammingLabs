using DictionaryLib.Models;
using System.ServiceModel;

namespace Lab4Client
{
    [ServiceContract]
    public interface IDictionaryContract
    {
        [OperationContract]
        string FindWord(string word);

        [OperationContract]
        string AddWord(Word word);
    }
}