using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;

namespace GPSCS // Namespace
{
    public class GPS2SVG
    {
        private const int Width = 1024;
        private const int Height = 1024;

        public static void Main(string[] args)
        {
            ValidateArguments(args); // Error handling for command line arguments

            var inputFilePath = args[0];
            var outputFilePath = args[1];

            ValidateInputFile(inputFilePath);

            var gpsDataList = ParseGPSData(inputFilePath); // Error handling checking if the file path exists or not.
            WriteSVGData(outputFilePath, gpsDataList);
        }

        // Encapsulation (Private) (HIDING INFORMATION)
        private static void ValidateArguments(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"Usage: dotnet run -- <inputGPS.txt> <outputSVG.svg>");
                Environment.Exit(1);
            }
        }

        private static void ValidateInputFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Can't open file {inputFilePath}");
                Environment.Exit(1);
            }
        }

        private static List<GPS> ParseGPSData(string inputFilePath)
        {
            var list = new List<GPS>(); // List for GPS handled natively through C# Chatgbt helped here
            foreach (var line in File.ReadLines(inputFilePath))
            {
                var gps = GPS.ParseNMEA0183Sentence(line);
                if (gps != null && gps.ValidSatelliteFix)
                {
                    list.Add(gps);
                }
            }
            return list;
        }

        private static GPS ParseSingleGPSData(string gpsDataLine)
        {
            return GPS.ParseNMEA0183Sentence(gpsDataLine);
        }

        private static void WriteSVGData(string outputFilePath, List<GPS> gpsDataList)
        {
            float LongMin = (float)gpsDataList.Min(g => g.Longitude);
            float LongMax = (float)gpsDataList.Max(g => g.Longitude);
            float latMin = (float)gpsDataList.Min(g => g.Latitude);
            float latMax = (float)gpsDataList.Max(g => g.Latitude);
            float LongWidth = LongMax - LongMin;
            float latWidth = latMax - latMin;

            var polylinePoints = string.Join(' ', gpsDataList.Select(g => 
                $"{Width * ((float)g.Longitude - LongMin) / LongWidth},{Height - Height * ((float)g.Latitude - latMin) / latWidth}"));

            var svgContent = @$"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
            <!DOCTYPE svg PUBLIC ""-//W3C//DTD SVG 1.1//EN"" ""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">
            <svg width=""{Width}"" height=""{Height}"" viewBox=""-50 -50 1124 1124"" xmlns=""http://www.w3.org/2000/svg"">
                <g opacity=""0.8"">
                    <polyline points=""{polylinePoints}"" stroke=""red"" stroke-width=""4"" fill=""none"" />
                </g>
            </svg>";

            File.WriteAllText(outputFilePath, svgContent);
        }
    }
}
