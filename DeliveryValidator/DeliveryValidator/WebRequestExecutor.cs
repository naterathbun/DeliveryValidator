using DeliveryValidator.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryValidator
{
    public class WebRequestExecutor
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<DeliveryEstimate> PostAsync(DeliveryEstimateRequest request, string url, string apiKey)
        {
            WebResponse response;

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {apiKey}" }
            };

            var requestJson = JsonConvert.SerializeObject(request);

            using (_client)
            {
                try
                {
                    var webRequest = BuildWebRequest("POST", url, requestJson, headers);
                    response =  webRequest.GetResponseAsync().Result;
                    return ProcessResponse(response);
                }
                catch (WebException ex)
                {
                    throw new WebRequestException(ex);
                }
            }
        }

        private HttpWebRequest BuildWebRequest(string method, string url, string content, Dictionary<string, string> headers)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = method;
            webRequest.Accept = "*/*";
            webRequest.ContentType = "application/json";
            //webRequest.UseDefaultCredentials = true;

            if (headers != null)
            {
                foreach (var key in headers.Keys)
                    webRequest.Headers.Add(key, headers[key]);
            }

            if (!string.IsNullOrEmpty(content))
            {
                var data = Encoding.UTF8.GetBytes(content);
                webRequest.ContentLength = data.Length;

                using (var writer = webRequest.GetRequestStream())
                {
                    writer.Write(data, 0, data.Length);
                    writer.Close();
                }
            }
            else
                webRequest.ContentLength = 0;

            return webRequest;
        }

        private DeliveryEstimate ProcessResponse(WebResponse response)
        {
            var responseString = "";

            using (var webResponseStream = response.GetResponseStream())
            {
                if (webResponseStream != null)
                {
                    using (var reader = new StreamReader(webResponseStream))
                    {
                        responseString = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            return JsonConvert.DeserializeObject<DeliveryEstimate>(responseString);
        }
    }
}
