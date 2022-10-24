using TestServer_Data.Domain;
using TestServer_Service.Models;

namespace TestServer_Service.Interfaces
{
    public interface IUserInputService
    {
        List<UserInputModel> GetSavedInputsAll();
        Task<UserInput?> GetUserInputByIdAsync(int id);
        Task<UserInput?> SaveUserInputAsync(UserInput userInput);
        Task<int> UpdateUserInputAsync(UserInput userInput);
    }
}
