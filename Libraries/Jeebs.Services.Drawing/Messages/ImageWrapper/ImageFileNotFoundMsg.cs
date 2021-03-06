// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jm.Services.Drawing.ImageWrapper
{
	/// <summary>
	/// Image File Not Found
	/// </summary>
	public sealed class ImageFileNotFoundMsg : NotFoundMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="imagePath">Image file path</param>
		public ImageFileNotFoundMsg(string imagePath) : base(imagePath) { }
	}
}
