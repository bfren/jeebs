// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

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

	#region Default Mime Types

	/// <summary>
	/// Blank / unknown
	/// </summary>
	public static readonly MimeType Blank = new(string.Empty);

	/// <summary>
	/// General
	/// </summary>
	public static readonly MimeType General = new("application/octet-stream");

	/// <summary>
	/// Bitmap
	/// </summary>
	public static readonly MimeType Bmp = new("image/bmp");

	/// <summary>
	/// Word document
	/// </summary>
	public static readonly MimeType Doc = new("application/msword");

	/// <summary>
	/// Word document (new format)
	/// </summary>
	public static readonly MimeType Docx = new("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

	/// <summary>
	/// Graphics Interchange Format
	/// </summary>
	public static readonly MimeType Gif = new("image/gif");

	/// <summary>
	/// Jpeg
	/// </summary>
	public static readonly MimeType Jpg = new("image/jpeg");

	/// <summary>
	/// MP4 audio
	/// </summary>
	public static readonly MimeType M4a = new("audio/mp4");

	/// <summary>
	/// MP3 audio
	/// </summary>
	public static readonly MimeType Mp3 = new("audio/mp3");

	/// <summary>
	/// PDF document
	/// </summary>
	public static readonly MimeType Pdf = new("application/pdf");

	/// <summary>
	/// Portable Network Graphics
	/// </summary>
	public static readonly MimeType Png = new("image/png");

	/// <summary>
	/// PowerPoint document
	/// </summary>
	public static readonly MimeType Ppt = new("application/vnd.ms-powerpoint");

	/// <summary>
	/// PowerPoint document (new format)
	/// </summary>
	public static readonly MimeType Pptx = new("application/vnd.openxmlformats-officedocument.presentationml.presentation");

	/// <summary>
	/// RAR
	/// </summary>
	public static readonly MimeType Rar = new("application/x-rar-compressed");

	/// <summary>
	/// TAR
	/// </summary>
	public static readonly MimeType Tar = new("application/x-tar");

	/// <summary>
	/// Text
	/// </summary>
	public static readonly MimeType Text = new("text/plain");

	/// <summary>
	/// Excel spreadsheet
	/// </summary>
	public static readonly MimeType Xls = new("application/vnd.ms-excel");

	/// <summary>
	/// Excel spreadsheet (new format)
	/// </summary>
	public static readonly MimeType Xlsx = new("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

	/// <summary>
	/// ZIP
	/// </summary>
	public static readonly MimeType Zip = new("application/zip");

	#endregion

	/// <summary>
	/// List of all mime types
	/// Must be static so it is thread safe
	/// </summary>
	private static readonly HashSet<MimeType> all;

	/// <summary>
	/// Populate list of mime types
	/// </summary>
	static MimeType() =>
		all = new HashSet<MimeType>(new[] { Blank, General, Bmp, Doc, Docx, Gif, Jpg, M4a, Mp3, Pdf, Png, Ppt, Pptx, Rar, Tar, Text, Xls, Xlsx, Zip });

	internal static HashSet<MimeType> AllTest() =>
		all;

	/// <summary>
	/// Add a custom mime types
	/// </summary>
	/// <param name="mimeType">Mime types to add</param>
	/// <returns>False if the mime type already exists</returns>
	public static bool AddCustomMimeType(MimeType mimeType) =>
		all.Add(mimeType);

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
