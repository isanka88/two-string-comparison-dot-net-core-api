using Microsoft.EntityFrameworkCore;
using TestServer_Data.Context;
using TestServer_Data.Domain;
using TestServer_Service.Interfaces;
using TestServer_Service.Models;

namespace TestServer_Service.Services
{
    public class UserInputService: IUserInputService
    {
        private readonly DataContext _dataContext;
        private readonly IComparisonService _ComparisonService;
        public UserInputService(IComparisonService ComparisonService,DataContext dataContext)
        {
            _dataContext = dataContext;
            _ComparisonService = ComparisonService;
        }
        public List<UserInputModel> GetSavedInputsAll()
        {
            var list = new List<UserInputModel>();
            _dataContext.UserInput.ToList().ForEach(input =>
            {
                var compare = _ComparisonService.CompareUserInput(input);
                list.Add(new UserInputModel
                {
                    InputId = input.InputId,
                    Left = input.Left,
                    Right = input.Right,
                    Status = compare.Status ? true : false,
                    DiffCount = compare.DiffCount,
                    Diff = compare.Diff,
                    Message = compare.Message
                });
            });

            return list;
        }
        public async Task<UserInput?> GetUserInputByIdAsync(int inputId)
        {
            return await _dataContext.UserInput.FirstOrDefaultAsync(x => x.InputId == inputId);
        }

        public async Task<UserInput?> SaveUserInputAsync(UserInput userInput)
        {
            try
            {
                _dataContext.UserInput.Add(userInput);
                await _dataContext.SaveChangesAsync();
                return userInput;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> UpdateUserInputAsync(UserInput userInput)
        {
            try
            {
                _dataContext.UserInput.Update(userInput);
               return await _dataContext.SaveChangesAsync();              
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
