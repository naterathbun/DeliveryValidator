using DeliveryValidator.Classes;
using System;

namespace DeliveryValidator
{
    public class Validator
    {
        private string _url = "https://api.doordash.com/drive/v1/estimates";
        private string _externalBusinessName = "TESTBUSINESS";
        private string _externalStoreId = "12345678-abcd-efgh-ijkl-1234567890ab";
        private int _orderValue = 1000;
                
        private DeliveryEstimateRequest _request;        

        public async void ValidateDelivery()
        {
            _request = GetDefaultRequest();

            Console.Write("\nEnter Api Key: ");
            var apiKey = Console.ReadLine();

            Console.WriteLine("\nEnter Pickup Address:");
            Console.WriteLine("---------------------");
            _request.pickup_address = CreateAddress();

            Console.WriteLine("\nEnter Dropoff Address:");
            Console.WriteLine("---------------------");
            _request.dropoff_address = CreateAddress();

            var executor = new WebRequestExecutor();
            var response = await executor.PostAsync(_request, _url, apiKey);

            if (response != null && response.delivery_time != null && response.pickup_time != null)
            {
                var timeEnroute = (response.delivery_time - response.pickup_time).TotalMinutes;                
                Console.WriteLine(String.Format("\nDoorDash will deliver from {0} to {1}. Approximate time enroute is {2} minutes.", _request.pickup_address.street, _request.dropoff_address.street, timeEnroute));
            }
            else            
                Console.WriteLine(String.Format("\nDoorDash will NOT deliver from {0} to {1}. Sorry :(", _request.pickup_address.street, _request.dropoff_address.street));            

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
    }
}