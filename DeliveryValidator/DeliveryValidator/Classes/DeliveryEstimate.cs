﻿using System;
using System.Collections.Generic;

namespace DeliveryValidator.Classes
{
    public class DeliveryEstimate
    {
        public string id { get; set; }
        public DateTime pickup_time { get; set; }
        public DateTime delivery_time { get; set; }
        public address dropoff_address { get; set; }
        public int fee { get; set; }
        public string currency { get; set; }
        public Dictionary<string, string>[] field_errors { get; set; }
    }
}