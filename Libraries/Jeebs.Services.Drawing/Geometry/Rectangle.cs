// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
