using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer_Data.Context;
using TestServer_Data.Domain;
using TestServer_Service.Interfaces;
using TestServer_Service.Models;
using TestServer_Service.Services;
using TestServer_xUnitTests.Context;

namespace TestServer_xUnitTests.ServiceTests
{
    public class UserInputServiceTests
    {
        private DataContextInMemory _databaseContext;
        private IComparisonService _ComparisonService;
        public UserInputServiceTests()
        {
            _databaseContext = new DataContextInMemory();

            //Dependencies
            _ComparisonService = A.Fake<IComparisonService>();
        }

        [Fact]
        public async void ComparisonService_GetSavedInputsAll_ReturnsUserInputModelList()
        {
            /// Expected test result - GetSavedInputsAll
            // -> Result cannot NULL
            // -> Count should be (5) same count as smple items

            var dbContext = await _databaseContext.GetDbContext();
            var userInputService = new UserInputService(_ComparisonService, dbContext);

            #region Insert sample data for data loading (5 items)
            dbContext.UserInput.Add(new UserInput() { InputId = 1, Left = "MatchingTextRow", Right = "MatchingTextRow" });
            dbContext.UserInput.Add(new UserInput() { InputId = 2, Left = "CountDiffText", Right = "CountDiff" });
            dbContext.UserInput.Add(new UserInput() { InputId = 3, Left = "DiffrentText", Right = "DiffrentTTTT" });
            dbContext.UserInput.Add(new UserInput() { InputId = 4, Left = "LeftTextOnly", Right = string.Empty });
            dbContext.UserInput.Add(new UserInput() { InputId = 5, Left = string.Empty, Right = "RightTextOnly" });
            dbContext.SaveChanges();
            #endregion


            // get the compate result
            var matchingTextRowResult = userInputService.GetSavedInputsAll();

            //Assert
            matchingTextRowResult.Should().NotBeNull();
            matchingTextRowResult.Should().BeOfType<List<UserInputModel>>();
            matchingTextRowResult.Count.Should().Be(5);
        }

        [Fact]
        public async void ComparisonService_SaveUserInputAsync_ReturnsBool()
        {
            /// Expected test result - SaveUserInputAsync
            // -> Result cannot NULL

            var userInput = new UserInput() { Id = 1, InputId = 2, Left = "CountDiffText", Right = "CountDiff" };

            var dbContext = await _databaseContext.GetDbContext();
            var userInputService = new UserInputService(_ComparisonService, dbContext);


            var result = await userInputService.SaveUserInputAsync(userInput);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<UserInput>>();

        }
       
        [Fact]
        public async void ComparisonService_GetUserInputByIdAsync_ReturnsUserInput()
        {
            /// Expected test result - GetUserInputByIdAsync
            // -> Result cannot NULL
            // -> Count should be (5) same count as smple items

            var id = 10;
            var dbContext = await _databaseContext.GetDbContext();
            var userInputService = new UserInputService(_ComparisonService, dbContext);

            //Act
            var result = userInputService.GetUserInputByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<UserInput>>();
        }
    }
}
