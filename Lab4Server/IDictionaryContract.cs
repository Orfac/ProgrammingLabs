using DictionaryLib.Models;
using System.ServiceModel;

namespace Lab4Server
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