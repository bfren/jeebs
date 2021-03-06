// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Size
	/// </summary>
	/// <param name="Height">Rectangle height</param>
	/// <param name="Width">Rectangle width</param>
	public record Size(int Height, int Width)
	{
		/// <summary>
		/// Ratio of Width / Height
		/// </summary>
		public double Ratio =>
			(double)Width / Height;
	}
}
