namespace GPSCS
{
    // Abstract Data Type: A prototype wasn't needed like in C
    public abstract class Coordinates //Namespace
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Latitude}, Longitude: {Longitude}";
        }

        // This should represent PolyMorphism
        public abstract void DisplayDetails();
    }
}
