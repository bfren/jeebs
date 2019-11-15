namespace Jeebs
{
	/// <summary>
	/// MimeType enumeration
	/// </summary>
	public class MimeType : Enum
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public MimeType(string name) : base(name) { }

		/// <summary>
		/// Blank / unknown
		/// </summary>
		public static MimeType Blank = new MimeType(string.Empty);

		/// <summary>
		/// General
		/// </summary>
		public static MimeType General = new MimeType("application/octet-stream");

		/// <summary>
		/// Bitmap
		/// </summary>
		public static MimeType Bmp = new MimeType("image/bmp");

		/// <summary>
		/// Word document
		/// </summary>
		public static MimeType Doc = new MimeType("application/msword");

		/// <summary>
		/// Word document (new format)
		/// </summary>
		public static MimeType Docx = new MimeType("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

		/// <summary>
		/// Graphics Interchange Format
		/// </summary>
		public static MimeType Gif = new MimeType("image/gif");

		/// <summary>
		/// Jpeg
		/// </summary>
		public static MimeType Jpg = new MimeType("image/jpeg");

		/// <summary>
		/// MP4 audio
		/// </summary>
		public static MimeType M4a = new MimeType("audio/mp4");

		/// <summary>
		/// MP3 audio
		/// </summary>
		public static MimeType Mp3 = new MimeType("audio/mp3");

		/// <summary>
		/// PDF document
		/// </summary>
		public static MimeType Pdf = new MimeType("application/pdf");

		/// <summary>
		/// Portable Network Graphics
		/// </summary>
		public static MimeType Png = new MimeType("image/png");

		/// <summary>
		/// PowerPoint document
		/// </summary>
		public static MimeType Ppt = new MimeType("application/vnd.ms-powerpoint");

		/// <summary>
		/// PowerPoint document (new format)
		/// </summary>
		public static MimeType Pptx = new MimeType("application/vnd.openxmlformats-officedocument.presentationml.presentation");

		/// <summary>
		/// RAR
		/// </summary>
		public static MimeType Rar = new MimeType("application/x-rar-compressed");

		/// <summary>
		/// TAR
		/// </summary>
		public static MimeType Tar = new MimeType("application/x-tar");

		/// <summary>
		/// Text
		/// </summary>
		public static MimeType Text = new MimeType("text/plain");

		/// <summary>
		/// Excel spreadsheet
		/// </summary>
		public static MimeType Xls = new MimeType("application/vnd.ms-excel");

		/// <summary>
		/// Excel spreadsheet (new format)
		/// </summary>
		public static MimeType Xlsx = new MimeType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

		/// <summary>
		/// ZIP
		/// </summary>
		public static MimeType Zip = new MimeType("application/zip");

		/// <summary>
		/// Parse MimeType value name
		/// </summary>
		/// <param name="name">Value name</param>
		/// <returns>MimeType object</returns>
		public static MimeType Parse(string name)
		{
			try
			{
				return Parse(name, new[]
				{
					Blank,
					General,
					Bmp,
					Doc,
					Docx,
					Gif,
					Jpg,
					M4a,
					Mp3,
					Pdf,
					Png,
					Ppt,
					Pptx,
					Rar,
					Tar,
					Text,
					Xls,
					Xlsx,
					Zip
				});
			}
			catch (Jx.ParseException)
			{
				return General;
			}
		}
	}
}
