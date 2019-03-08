using System;
using System.IO;
using System.Net;

namespace DeliveryValidator.Classes
{
    public class WebRequestException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; protected set; }
        public string Response { get; protected set; }
        public override string Message { get { return Response; } }

        public WebRequestException(WebException webException)
            : base(string.Empty, webException)
        {
            using (var errorWebResponse = webException.Response)
            {
                var httpResponse = (HttpWebResponse)errorWebResponse;
                if (httpResponse != null)
                {
                    using (var errorStream = httpResponse.GetResponseStream())
                    {
                        if (errorStream != null)
                        {
                            using (var reader = new StreamReader(errorStream))
                            {
                                this.Response = reader.ReadToEnd();
                                reader.Close();
                            }
                        }
                    }

                    this.HttpStatusCode = httpResponse.StatusCode;
                }
            }

            if (string.IsNullOrEmpty(this.Response))
                this.Response = webException.Message;
        }
    }
}