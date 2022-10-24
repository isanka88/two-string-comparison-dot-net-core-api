using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer_Service.Models
{ 
    /// <summary>
     /// Represents user inputs
     /// </summary>
    public class UserInputModel
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int InputId { get; set; }
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
        public string Message { get; set; }


        /// <summary>
        /// Gets or sets the matched status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the diff offset
        /// </summary>
        public List<string> Diff { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the diff offset conut
        /// </summary>
        public int DiffCount { get; set; }
    }
}
