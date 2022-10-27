namespace OpenCage.Geocode
{
    using System.Collections.Generic;

    public class GeocoderResponse : IGeocoderResponse
    {
        public GeocoderResponse()
        {
            this.Licenses = new List<License>();
            this.Results = new List<Location>();
        }

        public string Documentation { get; set; }
        public IEnumerable<License> Licenses { get; set; }
        public Rate Rate { get; set; }
        public IEnumerable<Location> Results { get; set; }
        public RequestStatus Status { get; set; }
        public Timestamp Timestamp { get; set; }
        public int TotalResults { get; set; }
        public EchoRequest Request { get; set; }
    }
}