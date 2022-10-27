namespace OpenCage.Geocode
{
    using System.Threading.Tasks;

    public interface IGeoCoder
    {
        Task<IGeocoderResponse> GeoCodeAsync(
            string query,
            string language = "en",
            string countrycode = null,
            Bounds bounds = null,
            bool abbreviated = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDeDuplication = false,
            bool noRecord = false,
            bool addRequest = false
        );

        Task<IGeocoderResponse> ReverseGeoCodeAsync(
            double latitude,
            double longitude,
            string language = "en",
            bool abbreviated = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDeDuplication = false,
            bool noRecord = false,
            bool addRequest = false
        );
    }
}