using Microsoft.AspNetCore.Mvc;
using TestServer_Data.Domain;
using TestServer_Service.Models;
using TestServer_Service.Interfaces;

namespace TestServer_API.Controllers
{
    [ApiController]
    [Route("v1/diff")]
    public class DiffController : ControllerBase
    {
        private readonly IUserInputService _userInputService;
        private readonly IComparisonService _comparisonService;

        public DiffController(IUserInputService userInputService, IComparisonService comparisonService)
        {
            _userInputService = userInputService;
            _comparisonService = comparisonService;
        }

        [HttpPost]
        [Route("{id}/left")]
        public async Task<JsonResult> LeftValue(int id, [FromBody] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new JsonResult(BadRequest());

            var userInput = await _userInputService.GetUserInputByIdAsync(id);
            if (userInput == null)
            {
                // Save left value
                bool result = await _userInputService.SaveUserInputAsync(new UserInput { InputId = id, Left = input, Right = string.Empty }) != null ? true : false;
                return new JsonResult(new { Status = result, Message = result ? "Left value Saved!" : "Error occur while saving left value!" });
            }
            else
            {
                // Update left value
                userInput.Left = input;
                bool result = await _userInputService.UpdateUserInputAsync(userInput) > 0 ? true : false;
                return new JsonResult(new { Status = result, Message = result ? "Left value updated!" : "Error occur while updating left value!" });
            }
        }

        [HttpPost]
        [Route("{id}/right")]
        public async Task<JsonResult> RightValue(int id, [FromBody] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new JsonResult(BadRequest());


            var userInput = await _userInputService.GetUserInputByIdAsync(id);
            if (userInput == null)
            {
                // Save right value
                bool result = await _userInputService.SaveUserInputAsync(new UserInput { InputId = id, Left = string.Empty, Right = input }) != null ? true : false;
                return new JsonResult(new { Status = result, Message = result ? "Right value Saved!" : "Error occur while saving right value!" });
            }
            else
            {
                // Update right value
                userInput.Right = input;
                bool result = await _userInputService.UpdateUserInputAsync(userInput) > 0 ? true : false;
                return new JsonResult(new { Status = result, Message = result ? "Right value updated!" : "Error occur while updating right value!" });
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<JsonResult> GetCompared(int id)
        {
            var userInput = await _userInputService.GetUserInputByIdAsync(id);
            if (userInput == null)
                return new JsonResult(new CompareResultModel { Message = "Input values not available for this Id", Status = false, InputId = userInput.InputId, Left = userInput.Left, Right = userInput.Right });
            else if (string.IsNullOrEmpty(userInput.Left))
                return new JsonResult(new CompareResultModel { Message = "The left value for this ID is empty!", Status = false, InputId = userInput.InputId, Left = userInput.Left, Right = userInput.Right });
            else if (string.IsNullOrEmpty(userInput.Right))
                return new JsonResult(new CompareResultModel { Message = "The right value for this ID is empty!", Status = false, InputId = userInput.InputId, Left = userInput.Left, Right = userInput.Right });
            else
                return new JsonResult(_comparisonService.CompareUserInput(userInput));
        }

        [HttpGet]
        [Route("get-saved-inputs")]
        public JsonResult GetSavedInputsAll()
        {
            return new JsonResult(_userInputService.GetSavedInputsAll());
        }
    }
}