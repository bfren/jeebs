using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// MimeType enumeration
	/// </summary>
	public class MimeType : Enumerated
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public MimeType(string name) : base(name) { }

		/// <summary>
		/// Blank / unknown
		/// </summary>
		public static MimeType Blank { get; } = new MimeType(string.Empty);

		/// <summary>
		/// General
		/// </summary>
		public static MimeType General { get; } = new MimeType("application/octet-stream");

		/// <summary>
		/// Bitmap
		/// </summary>
		public static MimeType Bmp { get; } = new MimeType("image/bmp");

		/// <summary>
		/// Word document
		/// </summary>
		public static MimeType Doc { get; } = new MimeType("application/msword");

		/// <summary>
		/// Word document (new format)
		/// </summary>
		public static MimeType Docx { get; } = new MimeType("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

		/// <summary>
		/// Graphics Interchange Format
		/// </summary>
		public static MimeType Gif { get; } = new MimeType("image/gif");

		/// <summary>
		/// Jpeg
		/// </summary>
		public static MimeType Jpg { get; } = new MimeType("image/jpeg");

		/// <summary>
		/// MP4 audio
		/// </summary>
		public static MimeType M4a { get; } = new MimeType("audio/mp4");

		/// <summary>
		/// MP3 audio
		/// </summary>
		public static MimeType Mp3 { get; } = new MimeType("audio/mp3");

		/// <summary>
		/// PDF document
		/// </summary>
		public static MimeType Pdf { get; } = new MimeType("application/pdf");

		/// <summary>
		/// Portable Network Graphics
		/// </summary>
		public static MimeType Png { get; } = new MimeType("image/png");

		/// <summary>
		/// PowerPoint document
		/// </summary>
		public static MimeType Ppt { get; } = new MimeType("application/vnd.ms-powerpoint");

		/// <summary>
		/// PowerPoint document (new format)
		/// </summary>
		public static MimeType Pptx { get; } = new MimeType("application/vnd.openxmlformats-officedocument.presentationml.presentation");

		/// <summary>
		/// RAR
		/// </summary>
		public static MimeType Rar { get; } = new MimeType("application/x-rar-compressed");

		/// <summary>
		/// TAR
		/// </summary>
		public static MimeType Tar { get; } = new MimeType("application/x-tar");

		/// <summary>
		/// Text
		/// </summary>
		public static MimeType Text { get; } = new MimeType("text/plain");

		/// <summary>
		/// Excel spreadsheet
		/// </summary>
		public static MimeType Xls { get; } = new MimeType("application/vnd.ms-excel");

		/// <summary>
		/// Excel spreadsheet (new format)
		/// </summary>
		public static MimeType Xlsx { get; } = new MimeType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

		/// <summary>
		/// ZIP
		/// </summary>
		public static MimeType Zip { get; } = new MimeType("application/zip");

		/// <summary>
		/// List of all mime types
		/// Must be public static so it is thread safe
		/// </summary>
		private static readonly HashSet<MimeType> all;

		/// <summary>
		/// Populate list of mime types
		/// </summary>
		static MimeType()
			=> all = new HashSet<MimeType>(new[] { Blank, General, Bmp, Doc, Docx, Gif, Jpg, M4a, Mp3, Pdf, Png, Ppt, Pptx, Rar, Tar, Text, Xls, Xlsx, Zip });

		/// <summary>
		/// Add a custom mime types
		/// </summary>
		/// <param name="mimeType">Mime types to add</param>
		/// <returns>False if the mime type already exists</returns>
		public static bool AddCustomMimeType(MimeType mimeType)
			=> all.Add(mimeType);

		/// <summary>
		/// Parse MimeType value
		/// </summary>
		/// <param name="mimeType">Value</param>
		public static MimeType Parse(string? mimeType)
		{
			// Return Blank for null
			if (mimeType is null)
			{
				return Blank;
			}

			// Parse and return value - if None, return General
			return Parse(mimeType, all.ToArray()).Unwrap(() => General);
		}
	}
}
