using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient__ServicesLibrary.Models
{
    /// <summary>
    /// Represent save/update status
    /// </summary>
    public class SaveReponseModel
    {
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
