using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Services.Drawing.Structs
{
	/// <summary>
	/// Size
	/// </summary>
	public struct Size
	{
		/// <summary>
		/// Height
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Width
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Ratio of Width / Height
		/// </summary>
		public double Ratio { get => (double)Width / Height; }
	}
}
