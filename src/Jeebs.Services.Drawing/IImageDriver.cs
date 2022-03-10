// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using MaybeF;

namespace Jeebs.Services.Drawing;

/// <summary>
/// Image Driver
/// </summary>
public interface IImageDriver
{
	/// <summary>
	/// Create image object from a file path
	/// </summary>
	/// <param name="path">File path</param>
	Maybe<IImageWrapper> FromFile(string path);

	/// <summary>
	/// Create image object from a stream
	/// </summary>
	/// <param name="stream">Stream</param>
	IImageWrapper FromStream(Stream stream);
}
