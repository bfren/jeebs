using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Rectangle
	/// </summary>
	/// <param name="X">X co-ordinate</param>
	/// <param name="Y">Y co-ordinate</param>
	/// <param name="Width">Rectangle width</param>
	/// <param name="Height">Rectangle height</param>
	public record Rectangle(int X, int Y, int Width, int Height);
}
