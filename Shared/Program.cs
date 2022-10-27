namespace GeocoderDemo
{
    using System;
    using OpenCage.Geocode;
    using ServiceStack.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var geoCoder = new GeoCoder("6edb02baf7644c54b5d881315c262844");
            // simplest example with no optional parameters
            var result = geoCoder.GeoCodeAsync("newcastle");

            result.PrintDump();

            //  example with lots of optional parameters
            var result2 = geoCoder.GeoCodeAsync("newcastle", countrycode: "gb", limit: 2, minConfidence: 6, language: "en",
                abbreviated: true, noAnnotations: true, noRecord: true, addRequest: true);

            result2.PrintDump();

            var reverseGeoCodeAsync = geoCoder.ReverseGeoCodeAsync(51.4277844, -0.3336517);

            reverseGeoCodeAsync.PrintDump();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}