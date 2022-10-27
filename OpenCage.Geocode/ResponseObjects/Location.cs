namespace OpenCage.Geocode
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Location
    {
        [DataMember(Name = "Annotations")] public Annotations Annotations { get; set; }

        [DataMember(Name = "Formatted")] public string Formatted { get; set; }

        [DataMember(Name = "Components")] public Dictionary<string, string> ComponentsDictionary { get; set; }

        public AddressComponent Components =>
            new AddressComponent
            {
                BusStop = this.ComponentsDictionary.GetValueOrDefault("bus_stop"),
                City = this.ComponentsDictionary.GetValueOrDefault("city"),
                Country = this.ComponentsDictionary.GetValueOrDefault("country"),
                County = this.ComponentsDictionary.GetValueOrDefault("county"),
                CountryCode = this.ComponentsDictionary.GetValueOrDefault("country_code"),
                Postcode = this.ComponentsDictionary.GetValueOrDefault("postcode"),
                Road = this.ComponentsDictionary.GetValueOrDefault("road"),
                State = this.ComponentsDictionary.GetValueOrDefault("state"),
                StateDistrict = this.ComponentsDictionary.GetValueOrDefault("state_district"),
                Suburb = this.ComponentsDictionary.GetValueOrDefault("suburb"),
                Type = this.ComponentsDictionary.GetValueOrDefault("_type")
            };

        [DataMember(Name = "Geometry")] public Point Geometry { get; set; }

        [DataMember(Name = "Bounds")] public Bounds Bounds { get; set; }

        [DataMember(Name = "Confidence")] public int Confidence { get; set; }
    }
}