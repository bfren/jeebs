using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Twitter.Models
{
	/// <summary>
	/// Twitter Author
	/// </summary>
	public sealed class AuthorModel
	{
		/// <summary>
		/// ScreenName
		/// </summary>
		public string ScreenName { get; set; } = string.Empty;

		/// <summary>
		/// FullName
		/// </summary>
		public string FullName { get; set; } = string.Empty;

		/// <summary>
		/// ProfileUrl
		/// </summary>
		public string ProfileUrl { get; set; } = string.Empty;

		/// <summary>
		/// ProfileImageUrl
		/// </summary>
		public string ProfileImageUrl { get; set; } = string.Empty;
	}
}
