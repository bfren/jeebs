using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Size
	/// </summary>
	public record Size(int Height, int Width)
	{
		/// <summary>
		/// Ratio of Width / Height
		/// </summary>
		public double Ratio =>
			(double)Width / Height;
	}
	//{
	//	/// <summary>
	//	/// Height
	//	/// </summary>
	//	public int Height { get; set; }

	//	/// <summary>
	//	/// Width
	//	/// </summary>
	//	public int Width { get; set; }
	//}
}
