namespace GPSCS
{
    // Closest thing to a prototype in C# I couldn't think of. C# doesn't need header files.
    
    // Abstract Data Type
    public abstract class Coordinates //Namespace
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString() // Override
        {
            return $"Latitude: {Latitude}, Longitude: {Longitude}";
        }

        // This should represent PolyMorphism
        public abstract void DisplayDetails();
    }
}
