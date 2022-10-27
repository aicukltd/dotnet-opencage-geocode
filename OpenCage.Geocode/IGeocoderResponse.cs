namespace OpenCage.Geocode
{
    using System.Collections.Generic;

    public interface IGeocoderResponse
    {
        string Documentation { get; set; }
        IEnumerable<License> Licenses { get; set; }
        Rate Rate { get; set; }
        IEnumerable<Location> Results { get; set; }
        RequestStatus Status { get; set; }
        Timestamp Timestamp { get; set; }
        int TotalResults { get; set; }
        EchoRequest Request { get; set; }
    }
}