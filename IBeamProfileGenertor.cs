﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Rhino.Geometry;

namespace IbeamPluginProject
{
    /// <summary>
    /// Generates profile curves
    /// </summary>
    internal interface IBeamProfileGenerator
    {
        /// <summary>
        /// Core behavior method that generates the profile polyline curve.
        /// NOTE: The profile polyline must be planar.
        /// </summary>
        /// <returns></returns>
        Polyline Generate(Brep brep, double x, double y, double distance);
    }
}
