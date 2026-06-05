using System;

namespace ElectricalCableManagement.Models
{
    public class RoadLength
    {
        public int ID { get; set; }
        public string SpaceName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public decimal StartPoint { get; set; }
        public decimal EndPoint { get; set; }
        public decimal TotalLength { get; set; }
        public string DrumNo { get; set; }
        public string DrumSerial { get; set; }
        public string RoadName { get; set; }
        public string MV { get; set; }
        public DateTime WorkDate { get; set; }
        public string Remarks { get; set; }
    }
}
