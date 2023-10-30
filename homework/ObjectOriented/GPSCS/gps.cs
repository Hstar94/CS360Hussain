using System;
using System.Globalization;

namespace GPSCS //Namespace
{
    // Enumeration to define type though in here we are only using GPRMC
    public enum GpsMessageType
    {
        GPRMC
    }
    // Ported and split from gps.c (struct and IF statements)
    // Data and Process Abstraction as this should limit the data entry if I did it correctly
    public class GPS : Coordinates
    {
        public GpsMessageType MessageType { get; set; }
        public string GpsTime { get; set; }
        public string GpsDate { get; set; }
        public bool ValidSatelliteFix { get; set; }
        public float Sog { get; set; } // Speed Over Ground
        public float Cmg { get; set; } // Course Made Good

        // Overriding (Generated by Chatgbt as well I had to adjust it though)
        public override void DisplayDetails()
        {
            Console.WriteLine($"GPS Details - {ToString()}, Time: {GpsTime}, ValidSatelliteFix: {ValidSatelliteFix}");
        }
        // Parameterization This takes a string and returns a GPS object
        public static GPS ParseNMEA0183Sentence(string line)
        {
            if (string.IsNullOrEmpty(line) || line[0] != '$') return null;

            string[] tokens = line.Split(',');
            if (tokens.Length == 0 || tokens[0] != "$GPRMC") return null;

            var gps = new GPS() { MessageType = GpsMessageType.GPRMC };
            if (tokens.Length < 10) return null;

            gps.GpsTime = tokens[1];
            gps.ValidSatelliteFix = tokens[2] == "A"; 
            gps.Latitude = ParseLatLon(tokens[3], tokens[4]);
            gps.Longitude = ParseLatLon(tokens[5], tokens[6]);
            gps.Sog = !string.IsNullOrEmpty(tokens[7]) ? float.Parse(tokens[7], CultureInfo.InvariantCulture) : 0;
            gps.Cmg = !string.IsNullOrEmpty(tokens[8]) ? float.Parse(tokens[8], CultureInfo.InvariantCulture) : 0;
            gps.GpsDate = tokens[9];

            return gps; 
        }

        private static double ParseLatLon(string coord, string dir)
        {
            if (string.IsNullOrEmpty(coord)) return 0;

            double val = double.Parse(coord, CultureInfo.InvariantCulture);
            double deg = Math.Floor(val / 100.0);

            double result = deg + (val - 100.0 * deg) / 60.0;
            return (dir == "S" || dir == "W") ? -result : result;
        }
    }
}
