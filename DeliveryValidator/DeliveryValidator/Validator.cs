using DeliveryValidator.Classes;
using System;

namespace DeliveryValidator
{
    public class Validator
    {
        private readonly string _url = "https://api.doordash.com/drive/v1/estimates";
        private readonly string _externalBusinessName = "Nathan";
        private readonly string _externalStoreId = "12345678-abcd-efgh-ijkl-1234567890ab";
        private readonly int _orderValue = 1000;

        private DeliveryEstimateRequest _request;
        private DeliveryEstimate _response;
        private string _apiKey;

        public void ValidateDelivery()
        {
            _request = GetDefaultRequest();

            Console.Write("\nEnter Api Key: ");
            _apiKey = Console.ReadLine();

            Console.WriteLine("\nEnter Pickup Address:");
            Console.WriteLine("---------------------");
            _request.PickupAddress = CreateAddress();

            Console.WriteLine("\nEnter Dropoff Address:");
            Console.WriteLine("---------------------");
            _request.DropoffAddress = CreateAddress();
            



            

        }

        private DeliveryEstimateRequest GetDefaultRequest()
        {
            return new DeliveryEstimateRequest()
            {
                PickupTime = DateTime.UtcNow.AddMinutes(60),
                ExternalStoreId = _externalStoreId,
                ExternalBusinessName = _externalBusinessName,
                OrderValue = _orderValue
            };
        }

        private Address CreateAddress()
        {
            var address = new Address();

            Console.Write("Street # and Name: ");
            address.Street = Console.ReadLine();
            Console.Write("City Name: ");
            address.City = Console.ReadLine();
            Console.Write("State Abbreviation: ");
            address.State = Console.ReadLine();
            Console.Write("Zip Code: ");
            address.ZipCode = Console.ReadLine();

            return address;
        }
    }
}