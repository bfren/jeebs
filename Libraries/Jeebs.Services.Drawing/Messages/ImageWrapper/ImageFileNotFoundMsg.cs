using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;

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
