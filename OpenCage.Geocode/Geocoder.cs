namespace OpenCage.Geocode
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using ServiceStack;

    public class GeoCoder : IGeoCoder
    {
        private const string BaseUrl =
            "https://api.opencagedata.com/geocode/v1/json?q={0}&key={1}&language={2}";

        private static readonly string DoubleFormat = "0." + new string('#', 339);

        private readonly string key;

        /// <summary>
        /// </summary>
        /// <param name="key">Your opencagedata geocoder key https://geocoder.opencagedata.com</param>
        public GeoCoder(string key)
        {
            this.key = key;
        }

        /// <summary>
        ///     Forward geocoding given a textual location
        /// </summary>
        /// <param name="query">The query string to be geocoded; a placename or lat+long.</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="countrycode">
        ///     Restricts the results to the specified country or countries. The country code is a two letter
        ///     code as defined by the ISO 3166-1 Alpha 2 standard.E.g.gb for the United Kingdom
        /// </param>
        /// <param name="bounds">
        ///     Provides the geocoder with a hint to the region that the query resides in. This value will
        ///     restrict the possible results to the supplied region. The bounds parameter should be specified as 4 coordinate
        ///     points forming the south-west and north-east corners of a bounding box.
        /// </param>
        /// <param name="abbreviated">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDeDuplication">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">
        ///     When set to true the query contents are not logged. Please use if you have concerns about
        ///     privacy and want us to have no record of your query.
        /// </param>
        /// <param name="addRequest">
        ///     When set to true the various request parameters are added to the response for ease of
        ///     debugging.
        /// </param>
        /// <returns></returns>
        public async Task<IGeocoderResponse> GeoCodeAsync(
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
            bool addRequest = false)
        {
            var url = string.Format(GeoCoder.BaseUrl, WebUtility.UrlEncode(query), this.key,
                WebUtility.UrlEncode(language));

            if (bounds != null)
                url +=
                    $"&bounds={bounds.NorthEast.Longitude}%2C{bounds.NorthEast.Latitude}%2C{bounds.SouthWest.Longitude}%2C{bounds.SouthWest.Latitude}";
            if (!string.IsNullOrEmpty(countrycode)) url += "&countrycode=" + countrycode;

            GeoCoder.AddCommonOptionalParameters(ref url, limit, minConfidence, noAnnotations, noDeDuplication, noRecord, abbreviated,
                addRequest);

            return await GeoCoder.GetResponseAsync(url);
        }

        /// <summary>
        ///     Reverse geocoding given a latitude and longitude
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="abbreviated">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDeDuplication">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">
        ///     When set to true the query contents are not logged. Please use if you have concerns about
        ///     privacy and want us to have no record of your query.
        /// </param>
        /// <param name="addRequest">
        ///     When set to true the various request parameters are added to the response for ease of
        ///     debugging.
        /// </param>
        /// <returns></returns>
        public async Task<IGeocoderResponse> ReverseGeoCodeAsync(
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
        )
        {
            var url = this.GetReverseGeocodeUrl(latitude, longitude, language, abbreviated, limit, minConfidence,
                noAnnotations, noDeDuplication, noRecord, addRequest);
            return await GeoCoder.GetResponseAsync(url);
        }

        private static void AddCommonOptionalParameters(ref string url, int limit, int minConfidence, bool noAnnotations,
            bool noDeDuplication, bool noRecord, bool abbreviated, bool addRequest)
        {
            if (limit > 0) url += "&limit=" + limit;
            if (minConfidence > 0) url += "&min_confidence=" + minConfidence;
            if (noAnnotations) url += "&no_annotations=1";
            if (noDeDuplication) url += "&no_dedupe=1";
            if (noRecord) url += "&no_record=1";
            if (abbreviated) url += "&abbrv=1";
            if (addRequest) url += "&add_request=1";
        }

        private string GetReverseGeocodeUrl(double latitude,
            double longitude,
            string language,
            bool abbreviated,
            int limit,
            int minConfidence,
            bool noAnnotations,
            bool noDeDuplication,
            bool noRecord,
            bool addRequest)
        {
            var url = this.GetReverseGeocodeUrl(latitude, longitude, language);
            GeoCoder.AddCommonOptionalParameters(ref url, limit, minConfidence, noAnnotations, noDeDuplication, noRecord, abbreviated,
                addRequest);
            return url;
        }

        protected string GetReverseGeocodeUrl(double latitude, double longitude, string language)
        {
            return string.Format(GeoCoder.BaseUrl,
                GeoCoder.ToNonScientificString(latitude) + "%2C" + GeoCoder.ToNonScientificString(longitude), this.key,
                WebUtility.UrlEncode(language));
        }

        protected static string ToNonScientificString(double d)
        {
            var s = d.ToString(GeoCoder.DoubleFormat, CultureInfo.InvariantCulture).TrimEnd('0');
            return s.Length == 0 ? "0.0" : s;
        }

        private static async Task<GeocoderResponse> GetResponseAsync(string url)
        {
            try
            {
                var result = await url.GetJsonFromUrlAsync();
                return result.FromJson<GeocoderResponse>();
            }
            catch (WebException webException)
            {
                var response = GeoCoder.ProcessExceptionGeoCoderResponse(webException);

                if (response != null) return response;

                throw;
            }
        }

        private static GeocoderResponse ProcessExceptionGeoCoderResponse(WebException webException)

        {
            // check if error can be returned as a http status
            if (!(webException.Response is HttpWebResponse response)) return null;

            var body = webException.GetResponseBody();
            try
            {
                var gcr = body.FromJson<GeocoderResponse>();
                switch ((int)response.StatusCode)
                {
                    case 400:
                        gcr.Status.Message =
                            "Invalid request (bad request; a required parameter is missing; invalid coordinates))";
                        break;
                    case 402:
                        gcr.Status.Message = "Valid request but quota exceeded (payment required)";
                        break;
                    case 403:
                        gcr.Status.Message = "Invalid or missing api key (forbidden)";
                        break;
                    case 429:
                        gcr.Status.Message = "Too many requests (too quickly, rate limiting)";
                        break;
                }

                return gcr;
            }
            catch (Exception ex)
            {
                return new GeocoderResponse
                    { Status = new RequestStatus { Code = (int)response.StatusCode, Message = ex.Message } };
            }
        }

        private GeocoderResponse GetResponse(string url)
        {
            try
            {
                var result = url.GetJsonFromUrl();
                return result.FromJson<GeocoderResponse>();
            }
            catch (WebException webException)
            {
                var response = GeoCoder.ProcessExceptionGeoCoderResponse(webException);

                if (response != null) return response;

                throw;
            }
        }
    }
}