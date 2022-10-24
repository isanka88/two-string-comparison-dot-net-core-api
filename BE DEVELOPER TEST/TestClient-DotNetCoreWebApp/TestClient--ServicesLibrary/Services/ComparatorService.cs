using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestClient__ServicesLibrary.Interfaces;
using TestClient__ServicesLibrary.Models;
using TestClient__ServicesLibrary.Util;

namespace TestClient__ServicesLibrary.Services
{
    public class ComparatorService : IComparatorService
    {

        public async Task<(List<SaveReponseModel> ,UserInputResultModel)> GetCompareResultAsync(UserInputModel model)
        {
            var responce = new List<SaveReponseModel>();

            // *** Call API endpoint 01 - Save left input value API request
            if (!string.IsNullOrWhiteSpace(model.Left))
                responce.Add(await SaveRequestAsync(model.Left, string.Format(ServiceDefaults.Left_Endpoint, model.InputId)));

            // *** Call API endpoint 02 - Save right input value API request
            if (!string.IsNullOrWhiteSpace(model.Right))
                responce.Add(await SaveRequestAsync(model.Right, string.Format(ServiceDefaults.Right_Endpoint, model.InputId)));

            // *** Call API endpoint 03 - compare inpur values API request
            var result = await CompareInputValuesAsync(model.InputId);

            // return result as tuple
            return (responce,result);
        }

        public async Task<UserInputResultModel> CompareInputValuesAsync(int inputId)
        {
            // this method will compare left and right values by input id
            var response = await WebRequestHandler.POSTRequestAsync(new WebRequestModel { APIEndPoint = $"{ServiceDefaults.Root_API}{string.Format(ServiceDefaults.CompareEndPoint_Endpoint, inputId)}" });
            return JsonConvert.DeserializeObject<UserInputResultModel>(response); ;
        }


        public async Task<List<UserInputResultModel>> GetSavedInputsAllAsync()
        {
            // this method will fetch all the saved input items in the SQl inmemonry database
            var response = await WebRequestHandler.GETRequestAsync(new WebRequestModel { APIEndPoint = $"{ServiceDefaults.Root_API}{ServiceDefaults.GetSavedInput_Endpoint}" });
            return JsonConvert.DeserializeObject<List<UserInputResultModel>>(response);
        }



        #region Utility
        private async Task<SaveReponseModel> SaveRequestAsync(string requestBody, string endPoint)
        {
            var result = await WebRequestHandler.POSTRequestAsync(
                   new WebRequestModel
                   {
                       APIEndPoint = $"{ServiceDefaults.Root_API}{endPoint}",
                       RequestBody = requestBody
                   });

            if (!string.IsNullOrEmpty(result))
                return JsonConvert.DeserializeObject<SaveReponseModel>(result);      
            else
                return new SaveReponseModel { Status = false, Message = "Network error. Please contact admin." };

        }
        #endregion
    }
}
