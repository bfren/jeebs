using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Services.Drawing
{
	/// <summary>
	/// Service for creating image objects
	/// </summary>
	public interface IImageSvc
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
