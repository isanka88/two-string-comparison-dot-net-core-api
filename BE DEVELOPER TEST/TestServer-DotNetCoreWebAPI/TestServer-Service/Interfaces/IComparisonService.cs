using TestServer_Data.Domain;
using TestServer_Service.Models;

namespace TestServer_Service.Interfaces
{
    public interface IComparisonService
    {
        CompareResultModel CompareUserInput(UserInput userInput);       
    }
}
