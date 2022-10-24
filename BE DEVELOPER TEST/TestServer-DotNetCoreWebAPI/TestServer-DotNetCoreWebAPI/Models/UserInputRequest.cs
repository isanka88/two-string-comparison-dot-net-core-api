using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer_API.Models
{
    public class UserInputRequest
    {
        /// <summary>
        /// Gets or sets the user input id
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the left value
        /// </summary>
        public string Left { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the right value
        /// </summary>
        public string Right { get; set; } = string.Empty;

    }
}
