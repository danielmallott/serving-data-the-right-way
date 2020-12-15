using System;
using System.Collections.Generic;

namespace ServingDataTheRightWay.Data.Models
{
    public partial class ColdRoomTemperature
    {
        public long Id { get; set; }
        public int ColdRoomSensorNumber { get; set; }
        public DateTime RecordedWhen { get; set; }
        public decimal Temperature { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
