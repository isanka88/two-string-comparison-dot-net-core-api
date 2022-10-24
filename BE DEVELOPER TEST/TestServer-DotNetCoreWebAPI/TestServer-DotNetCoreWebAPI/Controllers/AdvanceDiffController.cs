using Microsoft.AspNetCore.Mvc;
using TestServer_Data.Domain;
using TestServer_Service.Models;
using TestServer_Service.Interfaces;
using TestServer_API.Models;

namespace TestServer_API.Controllers
{
    [ApiController]
    [Route("v1/diff-advance")]
    public class AdvanceDiffController : ControllerBase
    {
        private readonly IUserInputService _userInputService;
        private readonly IComparisonService _comparisonService;

        public AdvanceDiffController(IUserInputService userInputService, IComparisonService comparisonService)
        {
            _userInputService = userInputService;
            _comparisonService = comparisonService;
        }

        [HttpPost]
        [Route("left-right-compare")]
        public async Task<JsonResult> StringCompare([FromBody] UserInputRequest request)
        {
            if (request == null)
                return new JsonResult(BadRequest());

            var saveStatus = false;
            var userInput = await _userInputService.GetUserInputByIdAsync(request.Id);
            if (userInput == null)
            {
                // Save 
                userInput = await _userInputService.SaveUserInputAsync(new UserInput { InputId = request.Id, Left = request.Left, Right = request.Right });
                saveStatus = userInput != null ? true : false;
            }
            else
            {
                // Update 
                userInput.Left = request.Left;
                userInput.Right = request.Right;
                saveStatus = await _userInputService.UpdateUserInputAsync(userInput) > 0 ? true : false;
            }

            var result = new CompareResultModel { InputId = request.Id, Left = request.Left, Right = request.Right };
            if (string.IsNullOrEmpty(request.Left))
            {
                result.Message = "The left value for this ID is empty!";
                result.Status = false;
            }
            else if (string.IsNullOrEmpty(request.Right))
            {
                result.Message = "The right value for this ID is empty!";
                result.Status = false;
            }
            else
                result = _comparisonService.CompareUserInput(new UserInput { InputId = request.Id, Left = request.Left, Right = request.Right });

            return new JsonResult(new { SaveResult = saveStatus, ComapreResult = result });
        }
    }
}