using DictionaryLib.Models;
using System.ServiceModel;

namespace DictionaryLib.Services
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