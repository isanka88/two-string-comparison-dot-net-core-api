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
