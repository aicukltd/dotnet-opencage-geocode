namespace Tests.NetCore
{
    using NUnit.Framework;
    using OpenCage.Geocode;

    public class GetReverseGeocodeUrlTests
    {
        private TestGeoCoder geoCoder;

        [SetUp]
        public void SetUp()
        {
            this.geoCoder = new TestGeoCoder("the-key");
        }

        [Test]
        public void WhenNearZero_ExpectCorrectUrl()
        {
            // Longitude of 0.000009 is converted to 9E-06 using Invariant ToString, but we need 0.000009
            Assert.AreEqual("https://api.opencagedata.com/geocode/v1/json?q=57.231%2C0.000009&key=the-key&language=en",
                this.geoCoder.GetReverseGeocodeUrlPublic(57.231d, 0.000009d, "en"));
        }

        [Test]
        public void WhenNearZero_ExpectCorrectToString()
        {
            // Longitude of 0.000009 is converted to 9E-06 using Invariant ToString, but we need 0.000009
            Assert.AreEqual("0.000009", TestGeoCoder.ToNonScientificStringPublic(0.000009d));
        }

        [Test]
        public void WhenZero_ExpectCorrectToString()
        {
            Assert.AreEqual("0.0", TestGeoCoder.ToNonScientificStringPublic(0.0d));
        }

        private class TestGeoCoder : GeoCoder
        {
            public TestGeoCoder(string key) : base(key)
            {
            }

            public string GetReverseGeocodeUrlPublic(double latitude, double longitude, string language)
            {
                return this.GetReverseGeocodeUrl(latitude, longitude, language);
            }

            public static string ToNonScientificStringPublic(double d)
            {
                return GeoCoder.ToNonScientificString(d);
            }
        }
    }
}