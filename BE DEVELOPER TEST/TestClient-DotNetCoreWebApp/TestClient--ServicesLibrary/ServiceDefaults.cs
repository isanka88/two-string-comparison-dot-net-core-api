using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient__ServicesLibrary
{
    /// <summary>
    /// Represents service constants
    /// </summary>
    public static class ServiceDefaults
    {
        /// <summary>
        /// Gets a name of the root url of the web API
        /// </summary>
        public static string Root_API => "https://localhost:7150/v1/";

        /// <summary>
        /// Gets a endpoint of the left POST request
        /// </summary>
        public static string Left_Endpoint => "diff/{0}/left";

        /// <summary>
        /// Gets a endpoint of the right POST request
        /// </summary>
        public static string Right_Endpoint => "diff/{0}/right";

        /// <summary>
        /// Gets a endpoint of the Compare End Point
        /// </summary>
        public static string CompareEndPoint_Endpoint => "diff/{0}";

        /// <summary>
        /// Gets a endpoint of the get saved inputs
        /// </summary>
        public static string GetSavedInput_Endpoint => "diff/get-saved-inputs";
    }
}
