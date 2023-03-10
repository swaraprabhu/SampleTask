using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IbeamPluginProject
{ 
  internal class BeamProfile :IBeamProfileGenerator
{
    public BeamProfile(double h, double w,double t)
    {
        Height = h;
        Width = w;
        Thickness = t;
    }

    public double Height { get; }
    public double Width { get; }
    public double Thickness { get; }

     
    /*public Polyline Generate(double h,double w, double t)
         {
            Rectangle3d r1 = new Rectangle3d(Plane.WorldXY, w, t);
             //Rectangle3d rectangle = new Rectangle3d(Plane.WorldXY, Width, Height);
             return r1.ToPolyline();
         }*/
       public Polyline Generate(Brep brep, double x, double y, double distance)
       
        {
            Polyline profile = new Polyline();
                double? max_z = null;
                var breps = new List<Brep> { brep };
                var points = new List<Point3d> { new Point3d(x, y, 0) };
                // grab all the points projected in Z dir.  Aggregate finds furthest Z from XY plane
                try
                {
                    max_z = (from pt in Intersection.ProjectPointsToBreps(breps, points, new Vector3d(0, 0, 1), distance) select pt.Z)
                            // Here you might be tempted to use .Max() to get the largest Z value but that doesn't work because
                            // Z might be negative.  This custom aggregate returns the max Z independant of the sign.  If it had a name
                            // it could be MaxAbs()
                            .Aggregate((z1, z2) => Math.Abs(z1) > Math.Abs(z2) ? z1 : z2);
                }
                catch (InvalidOperationException) {/*Sequence contains no elements*/}
            return profile;
            }

            private double? MaxZIntersectionMethod(Brep brep, double x, double y, double tolerance)
            {
                double? max_z = null;

                var bbox = brep.GetBoundingBox(true);
                var max_dist_from_xy = (from corner in bbox.GetCorners() select corner.Z)
                                        // furthest Z from XY plane.
                                        // Here you might be tempted to use .Max() to get the largest Z value but that doesn't work because
                                        // Z might be negative.  This custom aggregate returns the max Z independant of the sign.  If it had a name
                                        // it could be MaxAbs()
                                        .Aggregate((z1, z2) => Math.Abs(z1) > Math.Abs(z2) ? z1 : z2);
                // multiply distance by 2 to make sure line intersects completely
                var line_curve = new LineCurve(new Point3d(x, y, 0), new Point3d(x, y, max_dist_from_xy * 2));

                Curve[] overlap_curves;
                Point3d[] inter_points;
                if (Intersection.CurveBrep(line_curve, brep, tolerance, out overlap_curves, out inter_points))
                {
                    if (overlap_curves.Length > 0 || inter_points.Length > 0)
                    {
                        // grab all the points resulting frem the intersection. 
                        //    1st set: points from overlapping curves, 
                        //    2nd set: points when there was no overlap
                        //    .Aggregate: furthest Z from XY plane.
                        max_z = (from c in overlap_curves select Math.Abs(c.PointAtEnd.Z) > Math.Abs(c.PointAtStart.Z) ? c.PointAtEnd.Z : c.PointAtStart.Z)
                                .Union
                                (from p in inter_points select p.Z)
                                // Here you might be tempted to use .Max() to get the largest Z value but that doesn't work because
                                // Z might be negative.  This custom aggregate returns the max Z independant of the sign.  If it had a name
                                // it could be MaxAbs()
                                .Aggregate((z1, z2) => Math.Abs(z1) > Math.Abs(z2) ? z1 : z2);
                    }
                }
                return max_z;
            }
        }
    }


