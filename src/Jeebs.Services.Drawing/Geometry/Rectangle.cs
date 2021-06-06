// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Drawing.Geometry
{
	/// <summary>
	/// Rectangle
	/// </summary>
	/// <param name="X">X co-ordinate</param>
	/// <param name="Y">Y co-ordinate</param>
	/// <param name="Width">Rectangle width</param>
	/// <param name="Height">Rectangle height</param>
	public sealed record Rectangle(int X, int Y, int Width, int Height);
}
