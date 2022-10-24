using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using TestClient__ServicesLibrary.Interfaces;
using TestClient__ServicesLibrary.Models;
using TestClient__ServicesLibrary.Util;
using TestClient_DotNetCoreWebApp.Models;

namespace TestClient_DotNetCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComparatorService _ComparatorService;
        public HomeController(IComparatorService ComparatorService)
        {
            _ComparatorService = ComparatorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetSavedInputsAll()
        {
            var model = new UserInputViewModel();
            try
            {
                _ComparatorService.GetSavedInputsAllAsync().Result.ToList().ForEach(input =>
                {
                    model.Response.Add(new UserInputResponse
                    {
                        Id = input.InputId,
                        Left = input.Left,
                        Right = input.Right,
                        Status = input.Status ? true : false,
                        DiffCount = input.DiffCount,
                        Diff = string.Join(",", input.Diff),
                        Message = input.Message
                    });
                });
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = "Server error. Please contact admin.";

                // TODO : Error log
            }
            return PartialView("_InputListGrid", model);
        }

        [HttpPost]
        public async Task<JsonResult> CompareIputStrings(UserInputModel model)
        {
            var result = await _ComparatorService.GetCompareResultAsync(model);
            return Json(new { SaveResponse = result.Item1, CompareResponse = result.Item2 });
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult UnitTest()
        {
            return View();
        }

        public IActionResult Suggestions()
        {
            return View();
        }
        public IActionResult Limitations()
        {
            return View();
        }
        
    }
}