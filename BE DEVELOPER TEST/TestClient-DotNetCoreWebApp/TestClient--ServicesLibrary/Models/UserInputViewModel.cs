using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient__ServicesLibrary.Models
{
    /// <summary>
    /// Represents user inputs
    /// </summary>
    public class UserInputViewModel
    {
        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string Message { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the matched status
        /// </summary>
        public bool Status { get; set; }


        /// <summary>
        /// Gets or sets the input list
        /// </summary>
        public List<UserInputResponse> Response { get; set; } = new List<UserInputResponse>();
    }

    public class UserInputResponse
    {
        /// <summary>
        /// Gets or sets the id
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

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string Message { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the matched status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the diff offset
        /// </summary>
        public string Diff { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the diff offset conut
        /// </summary>
        public int DiffCount { get; set; }
    }
}
