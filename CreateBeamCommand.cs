using Eto.Forms;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IbeamPluginProject
{
    public class CreateBeamCommand : Rhino.Commands.Command
    {
        public CreateBeamCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static CreateBeamCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "CreateBeam";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // select start of the line
         
           
                        Rhino.Geometry.Point3d[] corners;

                        Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetRectangle(out corners);
                        if (rc != Rhino.Commands.Result.Success)
                           return rc;

                        Point3d pt1;
                        using (GetPoint getPointAction = new GetPoint())
                        {
                            getPointAction.SetCommandPrompt("Please input Bottom-Left point of rectangle");

                            if (getPointAction.Get() != GetResult.Point)
                            {
                                RhinoApp.WriteLine("No Bottom-left Point was selected.");
                                return getPointAction.CommandResult();
                            }

                            pt1 = getPointAction.Point();
                        }

                        Point3d pt3;
                        using (GetPoint getPointAction = new GetPoint())
                        {
                            getPointAction.SetCommandPrompt("Please input Top-Right Point of rectangle");
                            getPointAction.SetBasePoint(pt1, true);

                          //  getPointAction.DynamicDraw +=
                         //     (sender, e) => e.Display.DrawLine(pt1, e.CurrentPoint, System.Drawing.Color.DarkBlue);
                            if (getPointAction.Get() != GetResult.Point)
                            {
                                RhinoApp.WriteLine("No Top-Right point was selected.");
                                return getPointAction.CommandResult();
                            }
                            pt3= getPointAction.Point();
                        }
                        // Get the active view
                        Rhino.Display.RhinoView view = doc.Views.ActiveView;
                        if (view == null)
                            return Rhino.Commands.Result.Failure;

                      Rhino.Geometry.Point3d p2 = corners[2];
                        
                        // Create a plane from the corner points
                        Rhino.Geometry.Plane plane = new Rhino.Geometry.Plane(pt1, p2, pt3);


                        // Add a clipping plane object to the document
                        Guid id = doc.Objects.AddClippingPlane(plane, pt1.DistanceTo(p2), p2.DistanceTo(pt3), view.ActiveViewportID);
                        if (id != Guid.Empty)
                        {
                            doc.Views.Redraw();
                            return Rhino.Commands.Result.Success;
                        }
                        return Rhino.Commands.Result.Failure;
                    }
                }
            }


          /*
            var gs = new GetObject();
            gs.SetCommandPrompt("select surface");
            gs.GeometryFilter = ObjectType.Surface;
            gs.DisablePreSelect();
            gs.SubObjectSelect = false;
            gs.Get();
            if (gs.CommandResult() != Result.Success)
                return gs.CommandResult();
            // get the brep
            var brep = gs.Object(0).Brep();
            if (brep == null)
                return Result.Failure;

            // get X and Y
            double x = 0.0, y = 0.0;
            var rc = RhinoGet.GetNumber("value of top-right corner", true, ref x);
            if (rc != Result.Success)
                return rc;
            rc = RhinoGet.GetNumber("value of bottom-left Corner", true, ref y);
            if (rc != Result.Success)
                return rc;

            // an earlier version of this sample used a curve-brep intersection to find Z
            var maxZ = MaxZIntersectionMethod(brep, x, y, doc.ModelAbsoluteTolerance);

            // projecting points is another way to find Z
            var max_z = MaxZProjectionMethod(brep, x, y, doc.ModelAbsoluteTolerance);

            if (max_z != null)
            {
                RhinoApp.WriteLine("Maximum surface Z coordinate at X={0}, Y={1} is {2}", x, y, max_z);
                doc.Objects.AddPoint(new Point3d(x, y, max_z.Value));
                doc.Views.Redraw();
            }
            else
                RhinoApp.WriteLine("no maximum surface Z coordinate at X={0}, Y={1} found.", x, y);

            return Result.Success;
        }

       
    }
          */