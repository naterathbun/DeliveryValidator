using System;

namespace DeliveryValidator
{
    public class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("DoorDash Delivery Validator");
            Console.WriteLine("===========================");

            var validator = new Validator();
            validator.ValidateDelivery();

            Console.WriteLine("\n--- Complete ---");
            Console.ReadLine();
        }
    }
}