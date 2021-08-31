// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Size
	/// </summary>
	/// <param name="Width">Rectangle width</param>
	/// <param name="Height">Rectangle height</param>
	public readonly record struct Size(int Width, int Height)
	{
		/// <summary>
		/// Return as a rectangle of current <see cref="Width"/> and <see cref="Height"/>
		/// </summary>
		public Rectangle AsRectangle =>
			new(0, 0, Width, Height);

		/// <summary>
		/// Ratio of Width / Height
		/// </summary>
		public double Ratio =>
			(double)Width / Height;
	}
}
