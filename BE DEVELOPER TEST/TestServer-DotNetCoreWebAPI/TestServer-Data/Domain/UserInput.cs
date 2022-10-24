using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer_Data.Domain
{
    /// <summary>
    /// Represents UserInput batabase table entity
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the input id
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
