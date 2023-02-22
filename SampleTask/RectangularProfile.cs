using Rhino.Geometry;

namespace SampleTask
{
    /// <summary>
    /// Simple rectangular profile with height and width
    /// </summary>
    internal class RectangularProfile : IProfileGenerator
    {
        public RectangularProfile(double height, double width) 
        { 
            Height = height;
            Width = width;
        }

        public double Height { get; }
        public double Width { get; }

        public Polyline Generate()
        {
            Rectangle3d rectangle = new Rectangle3d(Plane.WorldXY, Width, Height);
            return rectangle.ToPolyline();
        }
    }
}
