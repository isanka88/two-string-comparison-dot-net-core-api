using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestServer_Data.Context;
using TestServer_Data.Domain;
using TestServer_Service.Interfaces;
using TestServer_Service.Models;
using TestServer_Service.Util;
using static System.Net.Mime.MediaTypeNames;

namespace TestServer_Service.Services
{
    public class ComparisonService : IComparisonService
    {
        private readonly DataContext _dataContext;
        public ComparisonService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
      

        public CompareResultModel CompareUserInput(UserInput userInput)
        {
            if (userInput.Left == userInput.Right)
                return new CompareResultModel { Message = "Inputs were equal.", Status = true, InputId = userInput.InputId, Left = userInput.Left, Right = userInput.Right };

            if (userInput.Left.Length != userInput.Right.Length)
                return new CompareResultModel { Message = "Inputs are of different size.", Status = false, InputId = userInput.InputId, Left = userInput.Left, Right = userInput.Right };


            var diff = StringComparator.Compare(userInput.Left, userInput.Right);
            return new CompareResultModel
            {
                InputId = userInput.InputId,
                Left = userInput.Left,
                Right = userInput.Right,
                Message = $"Inputs are having {diff.Count} different characters.",
                Status = false,
                Diff = diff,
                DiffCount = diff.Count
            };
        }

    }
}
