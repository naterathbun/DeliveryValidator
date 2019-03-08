using System;

namespace DeliveryValidator.Classes
{
    public class DeliveryEstimateRequest
    {
        public Address PickupAddress { get; set; }
        public Address DropoffAddress { get; set; }
        public DateTime PickupTime { get; set; }
        public int OrderValue { get; set; }
        public string ExternalBusinessName { get; set; }
        public string ExternalStoreId { get; set; }
    }
}