namespace OpenCage.Geocode
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class Bounds
    {
        public Bounds()
        {
        }

        /// <summary>
        ///     Initializes Bounds object from bounds-finder string
        ///     https://opencagedata.com/bounds-finder
        /// </summary>
        /// <param name="raw">String from bounds-finder containing min lon, min lat, max lon, max lat</param>
        public Bounds(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                throw new ArgumentException("String parameter can't be null or empty!", nameof(raw));

            var values = raw.Split(',')
                .Select(c => double.Parse(c, CultureInfo.InvariantCulture))
                .ToArray();

            if (values.Length < 4 || values.Length > 4)
                throw new ArgumentException("String contained more or less than 4 values", nameof(raw));

            this.NorthEast = new Point(values[3], values[2]);
            this.SouthWest = new Point(values[1], values[0]);
        }

        public Point SouthWest { get; set; }

        public Point NorthEast { get; set; }
    }
}