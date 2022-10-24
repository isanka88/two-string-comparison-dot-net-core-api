using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServer_Data.Context;
using TestServer_Data.Domain;
using Xunit;

namespace TestServer_xUnitTests.Context
{
    public class DataContextInMemory
    {
        internal async Task<DataContext> GetDbContext()
        {      
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();


            if (await databaseContext.UserInput.CountAsync() < 0)
            {
                // Matching Text Row
                databaseContext.UserInput.Add(new UserInput() { Id = 10, InputId = 1, Left = "MatchingTextRow", Right = "MatchingTextRow" });

                // Count Diff Text
                databaseContext.UserInput.Add(new UserInput() { Id = 20, InputId = 2, Left = "CountDiffText", Right = "CountDiff" });

                // Diffrent Text
                databaseContext.UserInput.Add(new UserInput() { Id = 30, InputId = 3, Left = "DiffrentText", Right = "DiffrentTTTT" });

                // Left Text Only
                databaseContext.UserInput.Add(new UserInput() { Id = 40, InputId = 4, Left = "LeftTextOnly", Right = string.Empty });

                // Right Text Only
                databaseContext.UserInput.Add(new UserInput() { Id = 50, InputId = 5, Left = string.Empty, Right = "RightTextOnly" });

                await databaseContext.SaveChangesAsync();

            }

            return databaseContext;
        }
    }
}
