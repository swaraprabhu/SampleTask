using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Rhino.Geometry;

namespace SampleTask
{
    /// <summary>
    /// Generates profile curves
    /// </summary>
    internal interface IProfileGenerator
    {
        /// <summary>
        /// Core behavior method that generates the profile polyline curve.
        /// NOTE: The profile polyline must be planar.
        /// </summary>
        /// <returns></returns>
        Polyline Generate();
    }
}
