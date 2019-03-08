using System;

namespace DeliveryValidator.Classes
{
    public class DeliveryEstimateRequest
    {
        public address pickup_address { get; set; }
        public address dropoff_address { get; set; }
        public DateTime pickup_time { get; set; }
        public int order_value { get; set; }
        public string external_business_name { get; set; }
        public string external_store_id { get; set; }
    }
}