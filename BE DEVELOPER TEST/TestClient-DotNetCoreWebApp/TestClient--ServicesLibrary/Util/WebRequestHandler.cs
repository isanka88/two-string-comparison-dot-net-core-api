using System.Net;
using System.Text;
using Newtonsoft.Json;
using TestClient__ServicesLibrary.Models;

namespace TestClient__ServicesLibrary.Util
{
    public static class WebRequestHandler
    {
        public static async Task<string> POSTRequestAsync(WebRequestModel model)
        {
            try
            {
                var response = string.Empty;
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    response = client.UploadString(model.APIEndPoint, JsonConvert.SerializeObject(model.RequestBody));
                }
                return response;
            }
            catch (WebException ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
            catch (Exception ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
        }

        public static async Task<string> GETRequestAsync(WebRequestModel model)
        {
            var response = string.Empty;
            try
            {
                WebRequest req = WebRequest.Create(model.APIEndPoint);
                req.Method = "GET";
                req.Timeout = int.MaxValue;

                HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse;
                using (var reader = new StreamReader(resp.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    response = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                // TODO : Error log;
                return string.Empty;
            }
            catch (Exception ex)
            {
                // TODO : Error log;
                return string.Empty;
            }

            return response;
        }
    }
}
