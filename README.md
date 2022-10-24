
 #  Two string comparison Web API & Web Client (.Net 6.0)

### Web API Overview
 HTTP endpoints that accept base64-encoded JSON of the following format.
 
 ### Requirement 
 Endpoint 01 - save/update left value 
 ```sh
- T<host>/v1/diff/<ID>/left
   - Texample: curl -X POST "<host>/v1/diff/<ID>/left" -H "accept: */*" -H "Content-Type:
application/custom" -d "\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\""
 ```
 
Endpoint 02 - save/update right value 
```sh
- T<host>/v1/diff/<ID>/right
   - Texample: curl -X POST "<host>/v1/diff/<ID>/right" -H "accept: */*" -H "Content-Type:
application/custom" -d "\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\""
 ```
 
Endpoint 03 - The provided JSON data needs to be diff-ed and the results shall be available on a third end poin
```sh
- T<host>/v1/diff/<ID>
 ```
 
Endpoint 04 - Returns all the saved user inputs
```sh
- T<host>/v1/diff/get-saved-inputs
   - Texample: curl -X 'GET' \ '<host>/v1/diff/get-saved-inputs' \  -H 'accept: */*'
 ```
 
[![N|Solid](https://e360b2bstorage.blob.core.windows.net/b2b-sportcar/images/0008101_0.png)](hhttps://github.com/isanka88)


## Comparison Logic
- The results shall provide the following info in JSON format
   - If the value of the "input" property of diffed JSON is equal, just return that information saying “inputs were equal”. No need to return compared values.
  - If the value of the "input" property of diffed JSONs is not of equal size, just return that information “inputs are of different size”. No
need to return compared values.
  - If the value of the "input" property of diffed JSONs has the same size, perform a simple diff - return offsets (in both values of the
"input" property) and lengths (in both values of the "input" property) of the differences.


## Software Requirement 
- [Visual Studio 2022] 


## Mainly followed, These Software Design Principles/Patterns 
- SOLID Principles
- OOP Concept
- Dependency Injection Pattern(DI) 

## Tech
These are the main frameworks or libraries used in the web API:
- [.net 6.0 framework] - .NET is an  open-source developer platform, created by Microsoft, for building many different types of applications.
- [Entity Framework Core] - open source and cross-platform version of the popular Entity Framework data access technology.
- [Microsoft.EntityFrameworkCore.InMemory] - This database provider allows Entity Framework Core to be used with an in-memory database. 
- [Swagger] - Open source API endpoint viewer to test API endpoints

## API Development/Architecture

Want to know project architecture? Great! 
It's very simple architecture!

API includes three projects under solution.
 
`[<sourse name>.API] .net 6.0 web API` project to implement all the web API endpoints
 
`[<sourse name>.Service] .net 6.0 class library ` project to manage all the logic and database requests
 
`[<sourse name>.Data] .net 6.0 class library ` project to Database context and entities


Check it out!
[![N|Solid](https://e360b2bstorage.blob.core.windows.net/b2b-sportcar/images/0008105_0.png)](https://github.com/isanka88)


>  #####  ** UseInMemoryDatabase (EF Core In-Memory Database Provider) to store user input data. Basically, It's a database but runs in the InMemory
> Read more about [EF Core In-Memory Database Provider]
https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli

### [TestServer_API] DiffController: _Contains all the API endpoints_

```sh
    [ApiController]
    [Route("v1/diff")]
    public class DiffController : ControllerBase
    {
        private readonly IComparisonService _comparisonService;
        public DiffController(IComparisonService comparisonService)
        {
            _comparisonService = comparisonService;
        }

        [HttpPost]
        [Route("{id}/left")]
        public async Task<JsonResult> LeftValue(int id, [FromBody] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new JsonResult(BadRequest());

            var userInput = await _comparisonService.GetUserInputByIdAsync(id);
            if (userInput.Id == 0)
            {
                // Save left value
                bool result = await _comparisonService.SaveUserInputAsync(new UserInput { InputId = id, Left = input, Right = string.Empty });
                return new JsonResult(new { Status = result, Message = result ? "Left value Saved!" : "Error occur while saving left value!" });
            }
            else
            {
                // Update left value
                userInput.Left = input;
                bool result = await _comparisonService.UpdateUserInputAsync(userInput);
                return new JsonResult(new { Status = result, Message = result ? "Left value updated!" : "Error occur while updating left value!" });
            }
        }

        [HttpPost]
        [Route("{id}/right")]
        public async Task<JsonResult> RightValue(int id, [FromBody] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new JsonResult(BadRequest());


            var userInput = await _comparisonService.GetUserInputByIdAsync(id);
            if (userInput.Id == 0)
            {
                // Save right value
                bool result = await _comparisonService.SaveUserInputAsync(new UserInput { InputId = id, Left = string.Empty, Right = input });
                return new JsonResult(new { Status = result, Message = result ? "Right value Saved!" : "Error occur while saving right value!" });
            }
            else
            {
                // Update right value
                userInput.Right = input;
                bool result = await _comparisonService.UpdateUserInputAsync(userInput);
                return new JsonResult(new { Status = result, Message = result ? "Right value updated!" : "Error occur while updating right value!" });
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<JsonResult> GetCompared(int id)
        {
            var userInput = await _comparisonService.GetUserInputByIdAsync(id);
            if (userInput.InputId == 0)
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
            return new JsonResult(_comparisonService.GetSavedInputsAll());
        }
    }
```


✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨✨



## Web Client Overview
>net 6.0 web application to call the API and test endpoints

- Display saved user inputs in a grid view by calling T<host>/v1/diff/get-saved-inputs
- Call T<host>/v1/diff/<ID>/left and save/update LEFT value
- Call T<host>/v1/diff/<ID>/right and save/update RIGHT value
- Call T<host>/v1/diff/<ID> and compare LEFT & RIGHT string values and display the result with diff offset 
- Display POST request (save/update) results as messages


[![N|Solid](https://e360b2bstorage.blob.core.windows.net/b2b-sportcar/images/0008100_0.png)](hhttps://github.com/isanka88)

## Tech
These are the main frameworks or libraries used in the web client:
- [.net 6.0 framework] - .NET is an open-source developer platform, created by Microsoft, for building many different types of applications.
- [jQuery] - handle GET/POST requests in the web client project.
- [Bootstrap] - Powerful, extensible, and feature-packed frontend toolkit. 

## Web Client Development/Architecture

Want to check out project architecture? Awesome! 
It's a very simple web application architecture!

Web client application includes two projects under solution.
 
`[<sourse name>.DotNetCoreWebApp] .net 6.0 web app` project to handle frontend
 
`[<sourse name>.Service] .net 6.0 class library ` project to manage all the logic and API requests



 > Check it out!
 
 
[![N|Solid](https://e360b2bstorage.blob.core.windows.net/b2b-sportcar/images/0008103_0.png)](https://github.com/isanka88)


### [TestClient_DotNetCoreWebApp] WebRequestHandler: _Handle GET/POST api requests_

```sh
    public static class WebRequestHandler
    {
        public static async Task<string> POSTRequestAsync(WebRequestModel model)
        {
            try
            {
                var response = string.Empty;
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    response = client.UploadString(model.APIEndPoint, JsonConvert.SerializeObject(model.RequestBody));
                }
                return response;
            }
            catch (WebException ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
            catch (Exception ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
        }

        public static async Task<string> GETRequestAsync(WebRequestModel model)
        {
            var response = string.Empty;
            try
            {
                WebRequest req = WebRequest.Create(model.APIEndPoint);
                req.Method = "GET";
                req.Timeout = int.MaxValue;

                HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse;
                using (var reader = new StreamReader(resp.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    response = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
            catch (Exception ex)
            {
                // TODO : Error log;
                return string.Empty;
            }

            return response;
        }
    }
```


### [TestClient_DotNetCoreWebApp]  _Sample AJAX POST request_

```sh
    function compareInputs() {

        var postData = {
            inputId: $('#inputId').val(),
            left: $('#left').val(),
            right: $('#right').val()
        };             

        $.ajax({
            cache: true,
            dataType: "json",
            method: "POST",
            url: "@(Url.Action("CompareIputStrings", "Home"))",
            data: postData,
            success: function (res) {

                // Display save/update messages
                renderSaveResult(res.saveResponse);

                // display compare result
                renderCompareResult(res.compareResponse);

                // refresh saved input list
                getSavedInputs();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("Error", textStatus);
            }
        });
    }
```




## Unit Test Overview (_xUnit_)
> Read more about [xUnit](https://xunit.net/)

## Tech
These are the main frameworks or libraries used in the web client:
- [.net 6.0 framework] - .NET is an open-source developer platform, created by Microsoft, for building many different types of applications.
- [xUnit(2.1.4)](https://xunit.net/) - Free, open source, community-focused unit testing tool for the .NET Framework.-
- [FluentAssertions.Json (6.1.0)](https://fluentassertions.com/) - Allow to more naturally specify the expected outcome of a TDD or BDD-style unit tests.
- [Microsoft.EntityFrameworkCore.InMemory] - This database provider allows Entity Framework Core to be used with an in-memory database. 

Checkout comparison service tests code  _ComparisonServiceTests.cs_
```sh
using FluentAssertions;
using TestServer_Data.Domain;
using TestServer_Service.Models;
using TestServer_Service.Services;
using TestServer_xUnitTests.Context;

namespace TestServer_xUnitTests.ServiceTests
{
    public class ComparisonServiceTests
    {
        private readonly DataContextInMemory _databaseContext;
        public ComparisonServiceTests()
        {
            _databaseContext = new DataContextInMemory();
        }

        [Fact]
        public async void ComparisonService_CompareUserInput_ReturnsCompareResultModel()
        {
            //Arrange
            var dbContext = await _databaseContext.GetDbContext();
            var comparisonService = new ComparisonService(dbContext);

            //Matching Text Row      
            #region Matching Text Row      

            /// Expected test result - Matching Text 
            // -> Result cannot NULL
            // -> Satatus = TRUE

            var matchingTextRow = new UserInput() { InputId = 1, Left = "MatchingTextRow", Right = "MatchingTextRow" };

            //  Insert test datase before compare
            dbContext.Add(matchingTextRow);
            dbContext.SaveChanges();

            // get the compate result
            var matchingTextRowResult = comparisonService.CompareUserInput(matchingTextRow);
            matchingTextRowResult.Should().NotBeNull();
            matchingTextRowResult.Should().BeOfType<CompareResultModel>();
            matchingTextRowResult.Status.Should().BeTrue();

            #endregion

            // Count Diff Text
            #region Count Diff Text

            /// Expected test result - Count Diff Text
            // -> Result cannot NULL
            // -> Satatus = FALSE
            // -> Message cannot NULL
            // -> DiffCount = 0

            var countDiffText = new UserInput() { InputId = 2, Left = "CountDiffText", Right = "CountDiff" };

            //  Insert test datase before compare
            dbContext.Add(countDiffText);
            dbContext.SaveChanges();

            // get the compate result
            var countDiffTextResult = comparisonService.CompareUserInput(countDiffText);
            countDiffTextResult.Should().NotBeNull();
            countDiffTextResult.Should().BeOfType<CompareResultModel>();
            countDiffTextResult.Status.Should().BeFalse();
            countDiffTextResult.Message.Should().NotBeNullOrEmpty();
            countDiffTextResult.DiffCount.Should().Be(0);
            #endregion

            // Diffrent Text
            #region Diffrent Text
            /// Expected test result - Diffrent Text
            // -> Result cannot NULL
            // -> Satatus = FALSE
            // -> Message cannot NULL
            // -> DiffCount = 6
            // -> Diff cannot NULL

            var diffrentText = new UserInput() { InputId = 3, Left = "DiffrentText", Right = "DiffrentTTTT" };

            //  Insert test datase before compare
            dbContext.Add(diffrentText);
            dbContext.SaveChanges();

            // get the compate result
            var diffrentTextResult = comparisonService.CompareUserInput(diffrentText);
            diffrentTextResult.Should().NotBeNull();
            diffrentTextResult.Should().BeOfType<CompareResultModel>();
            diffrentTextResult.Status.Should().BeFalse();
            diffrentTextResult.Message.Should().NotBeNullOrEmpty();
            diffrentTextResult.DiffCount.Should().Be(6); // onlt TTT is dif. so 3x2 = 6
            diffrentTextResult.Diff.Should().NotBeNull(); // diff offset should returns
            #endregion


            // Left Text Only
            #region Left Text Only

            /// Expected test result - Left Text Only
            // -> Result cannot NULL
            // -> Satatus = FALSE
            // -> Message cannot NULL

            var leftTextOnly = new UserInput() { InputId = 4, Left = "LeftTextOnly", Right = string.Empty };

            //  Insert test datase before compare
            dbContext.Add(leftTextOnly);
            dbContext.SaveChanges();

            // get the compate result
            var leftTextOnlyResult = comparisonService.CompareUserInput(leftTextOnly);
            leftTextOnlyResult.Should().NotBeNull();
            leftTextOnlyResult.Should().BeOfType<CompareResultModel>();
            leftTextOnlyResult.Status.Should().BeFalse();
            leftTextOnlyResult.Message.Should().NotBeNullOrEmpty();
            #endregion



            // Right Text Only
            #region Right Text Only

            /// Expected test result - Right Text Only
            // -> Result cannot NULL
            // -> Satatus = FALSE
            // -> Message cannot NULL

            var rightTextOnly = new UserInput() { InputId = 5, Left = string.Empty, Right = "RightTextOnly" };

            //  Insert test datase before compare
            dbContext.Add(rightTextOnly);
            dbContext.SaveChanges();

            // get the compate result
            var rightTextOnlyResult = comparisonService.CompareUserInput(rightTextOnly);
            rightTextOnlyResult.Should().NotBeNull();
            rightTextOnlyResult.Should().BeOfType<CompareResultModel>();
            rightTextOnlyResult.Status.Should().BeFalse();
            rightTextOnlyResult.Message.Should().NotBeNullOrEmpty();
            #endregion



        }

    }
}

```

#  Limitations

###  There few limitations in this project

- [Id] must be INT value
- Left & Right cannot NULL. Each POST request expects a request body.
- All three endpoints must return a successful response to get an accurate result.
- [Id]  is a redundant attribute. No need [Id]  value to compare left & right values
- Saving into a database is not necessary.  can compare left & right values



#  Suggestions

###  Develop a single API request

I would like to suggest implementing a single API end pint to serve the purpose of all these three API requests. Because those are calling from the same controller action and all three requests work with the same database table. And all three returns simple data sets.

There are more reasons such as,
- Response weights can be reduced to one response. In this case, it's small responses. But larger responses will increase network latency.
- Network latency can be reduced to one response.
- Three SQL queries for each request can be of Batase performance when it becomes a larger database and handle the large query load.
- Front-end issues can occur if one of the requests late to response in the sequence.
- Unit tests count will be less.
- Easy to maintain.
- Easy to narrow down issues when bug fixing.
- Development and testing time will be less.
- Data incomplete issue if one request failed.


Check it out!
[![N|Solid](https://e360b2bstorage.blob.core.windows.net/b2b-sportcar/images/0008783_0.png)](https://github.com/isanka88)

Source code
 ```sh
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
  ```       

## _Developed by Isanka Thalagala_
(Radio Liberty (RFE/RL) practicle assement for Back End Developer, .NET, C#)

[![N|Solid](https://lh3.googleusercontent.com/a/ALm5wu1qRjA2M1O0YLVP4R5wUkIi2lQRUR6FoAZfBqETSA=s83-c-mo)](https://github.com/isanka88)




## License

MIT

**Free Software, Hell Yeah!**




[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dill]: <https://github.com/isanka88/two-string-comparison-dot-net-core-api/settings>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Bootstrap]: <https://getbootstrap.com/docs/4.0/components/forms//>
   [Swagger]: <https://swagger.io/>
   [jQuery]: <http://jquery.com>
   [Entity Framework Core]: <https://learn.microsoft.com/en-us/ef/core/>
   [Microsoft.EntityFrameworkCore.InMemory]: <https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli>
   [.net 6.0 framework]: <https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6>
   [Visual Studio 2022]: <https://visualstudio.microsoft.com/vs/>
   [EF Core In-Memory Database Provider]: <https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli>



   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
`
 
