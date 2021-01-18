using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Jeebs.Reflection;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Parse the Mime Type of a filename using its extension
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>Mime Type, or 'application/octet-stream' if it cannot be detected</returns>
		public static string GetMimeFromExtension(this string @this) =>
			Modify(@this, () =>
			{
				// Get the index of the last period
				int lastPeriod = @this.LastIndexOf('.');
				if (lastPeriod == -1)
				{
					return @this;
				}

				// Get the extension and switch to get the mime type
				return (@this[(lastPeriod + 1)..].ToLowerInvariant()) switch
				{
					"bmp" =>
						MimeType.Bmp.ToString(),

					"doc" =>
						MimeType.Doc.ToString(),

					"docx" =>
						MimeType.Docx.ToString(),

					"gif" =>
						MimeType.Gif.ToString(),

					"jpg" or "jpeg" =>
						MimeType.Jpg.ToString(),

					"m4a" =>
						MimeType.M4a.ToString(),

					"mp3" =>
						MimeType.Mp3.ToString(),

					"pdf" =>
						MimeType.Pdf.ToString(),

					"png" =>
						MimeType.Png.ToString(),

					"ppt" =>
						MimeType.Ppt.ToString(),

					"pptx" =>
						MimeType.Pptx.ToString(),

					"rar" =>
						MimeType.Rar.ToString(),

					"tar" =>
						MimeType.Tar.ToString(),

					"txt" =>
						MimeType.Text.ToString(),

					"xls" =>
						MimeType.Xls.ToString(),

					"xlsx" =>
						MimeType.Xlsx.ToString(),

					"zip" =>
						MimeType.Zip.ToString(),

					_ =>
						MimeType.General.ToString(),
				};
			});
	}
}
