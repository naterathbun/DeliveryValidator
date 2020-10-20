using DeliveryValidator.Classes;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace DeliveryValidator
{
    public class Validator
    {
        private string _url = "https://api.doordash.com/drive/v1/estimates";
        private string _apiKey;
        private string _externalBusinessName;
        private string _externalStoreId = "12345678-abcd-efgh-ijkl-1234567890ab";
        private int _orderValue = 1000;

        private DeliveryEstimateRequest _request;

        public async void ValidateDelivery()
        {
            _request = GetDefaultRequest();

            Console.Write("\nEnter Api Key: ");
            _apiKey = Console.ReadLine();

            Console.Write("\nEnter External Business Name: ");
            _externalBusinessName = Console.ReadLine();

            Console.WriteLine("\nEnter Pickup Address:");
            Console.WriteLine("---------------------");
            _request.pickup_address = CreateAddress();

            Console.WriteLine("\nEnter Dropoff Address:");
            Console.WriteLine("---------------------");
            _request.dropoff_address = CreateAddress();

            var estimate = new DeliveryEstimate();
            var executor = new WebRequestExecutor();

            try
            {
                var response = await executor.PostAsync(_request, _url, _apiKey);
                estimate = ProcessResponse(response);
            }
            catch (Exception exception)
            {
                var webException = (WebException)exception.InnerException;
                var webRequestException = new WebRequestException(webException);
                estimate = null;
            }

            if (estimate != null && estimate.field_errors == null)
            {
                var timeEnroute = (estimate.delivery_time - estimate.pickup_time).TotalMinutes;
                Console.WriteLine(String.Format("\nDoorDash will deliver from {0} to {1}.\nApproximate time enroute is {2} minutes.", _request.pickup_address.street, _request.dropoff_address.street, timeEnroute));
            }
            else if (estimate != null)
            {
                Console.WriteLine(String.Format("\nDoorDash will NOT deliver from {0} to {1}.", _request.pickup_address.street, _request.dropoff_address.street));

                foreach (var error in estimate.field_errors)
                    Console.WriteLine(String.Format("Problem: {0}: {1}", error["field"], error["error"]));                
            }
            else
            {
                Console.WriteLine("\nERROR -- Problem with credentials or formatting.");
            }
            Console.ReadLine();
        }

        private DeliveryEstimateRequest GetDefaultRequest()
        {
            return new DeliveryEstimateRequest()
            {
                pickup_time = DateTime.UtcNow.AddMinutes(60),
                external_store_id = _externalStoreId,
                external_business_name = _externalBusinessName,
                order_value = _orderValue
            };
        }

        private address CreateAddress()
        {
            var address = new address();

            Console.Write("Street # and Name: ");
            address.street = Console.ReadLine();
            Console.Write("City Name: ");
            address.city = Console.ReadLine();
            Console.Write("State Abbreviation: ");
            address.state = Console.ReadLine();
            Console.Write("Zip Code: ");
            address.zip_code = Console.ReadLine();

            return address;
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