namespace OpenCage.Geocode
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Point
    {
        public Point()
        {
        }

        public Point(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        [DataMember(Name = "lat")] public double Latitude { get; set; }

        [DataMember(Name = "lng")] public double Longitude { get; set; }
    }
}