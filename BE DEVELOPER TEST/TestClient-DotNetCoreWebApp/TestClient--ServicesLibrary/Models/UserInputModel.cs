using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient__ServicesLibrary.Models
{
    public class UserInputModel
    { 
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

    }
}
