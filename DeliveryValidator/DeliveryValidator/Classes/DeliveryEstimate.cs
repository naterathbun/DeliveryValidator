using System;

namespace DeliveryValidator.Classes
{
    public class DeliveryEstimate
    {
        public string Id { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Address DropoffAddress { get; set; }
        public int Fee { get; set; }
        public string Currency { get; set; }
    }
}