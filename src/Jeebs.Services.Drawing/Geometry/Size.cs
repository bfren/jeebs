// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Size
	/// </summary>
	/// <param name="Height">Rectangle height</param>
	/// <param name="Width">Rectangle width</param>
	public sealed record Size(int Height, int Width)
	{
		/// <summary>
		/// Ratio of Width / Height
		/// </summary>
		public double Ratio =>
			(double)Width / Height;
	}
}
