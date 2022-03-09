// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Services.Drivers.Drawing.Skia;

/// <summary>
/// See <see cref="ImageFormatExtensions.GetEncodedImageFormat(Services.Drawing.ImageFormat)"/>
/// </summary>
public class UnsupportedImageFormatException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnsupportedImageFormatException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnsupportedImageFormatException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnsupportedImageFormatException(string message, Exception inner) : base(message, inner) { }
}
