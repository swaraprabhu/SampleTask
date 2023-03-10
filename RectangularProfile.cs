using Rhino.Geometry;

using System.Windows.Shapes;

namespace IbeamPluginProject
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
        
        public Rhino.Geometry.Polyline Generate()
        {
             Rectangle3d rectangle = new Rectangle3d(Plane.WorldXY, Width, Height);
             return rectangle.ToPolyline();
          
        }

        Rhino.Geometry.Polyline IProfileGenerator.Generate()
        {
            throw new System.NotImplementedException();
        }
    }
}

