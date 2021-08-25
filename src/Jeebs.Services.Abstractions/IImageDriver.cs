// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Drawing
{
	/// <summary>
	/// Image Driver
	/// </summary>
	public interface IImageDriver
	{
		/// <summary>
		/// Create image object from a file path
		/// </summary>
		/// <param name="path">File path</param>
		Option<IImageWrapper> FromFile(string path);

		/// <summary>
		/// Create image object from a stream
		/// </summary>
		/// <param name="stream">Stream</param>
		IImageWrapper FromStream(Stream stream);
	}
}
