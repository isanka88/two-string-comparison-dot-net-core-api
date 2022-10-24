using System;
using TestClient__ServicesLibrary.Models;

namespace TestClient__ServicesLibrary.Interfaces
{
    public interface IComparatorService
    {
        Task<(List<SaveReponseModel>, UserInputResultModel)> GetCompareResultAsync(UserInputModel model);
        Task<UserInputResultModel> CompareInputValuesAsync(int inputId);
        Task<List<UserInputResultModel>> GetSavedInputsAllAsync();
    }
}
