using DeliveryValidator.Classes;
using System;

namespace DeliveryValidator
{
    public class Validator
    {
        private DeliveryEstimateRequest _request;
        private DeliveryEstimate _response;

        public Validator()
        {
            _request = new DeliveryEstimateRequest();
            _response = new DeliveryEstimate();
        }

        public void ValidateDelivery()
        {
            Console.WriteLine("\nEnter Pickup Address:");
            Console.WriteLine("---------------------");
            _request.PickupAddress = CreateAddress();

            Console.WriteLine("\nEnter Dropoff Address:");
            Console.WriteLine("---------------------");
            _request.DropoffAddress = CreateAddress();
            



            

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