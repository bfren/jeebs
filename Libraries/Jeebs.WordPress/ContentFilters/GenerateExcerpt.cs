using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Jeebs.WordPress.ContentFilters
{
	/// <summary>
	/// Generate Excerpt
	/// </summary>
	public sealed class GenerateExcerpt : ContentFilter
	{
		/// <inheritdoc/>
		private GenerateExcerpt(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter with default max length
		/// </summary>
		public static ContentFilter Create()
			=> Create(200);

		/// <summary>
		/// Create filter
		/// </summary>
		/// <param name="maxLength">Maximum length of excerpt - ignored if <!--more--> is present</param>
		public static ContentFilter Create(int maxLength)
			=> new GenerateExcerpt(content =>
			{
				// If there's nothing there, return an empty string
				if (string.IsNullOrEmpty(content))
				{
					return string.Empty;
				}

				// Strip out square brackets
				var squareBrackets1 = new Regex(@"\[(\w+) (.*)\](.*)(\[\/(\1)\])", RegexOptions.Multiline);
				content = squareBrackets1.Replace(content, " ");

				var squareBrackets2 = new Regex(@"\[(\w+) (.*)\]", RegexOptions.Multiline);
				content = squareBrackets1.Replace(content, " ");

				// Strip out new lines
				var newLines = new Regex(@"\n");
				content = newLines.Replace(content, " ");

				// Cut out everything after <!--more--> tag, or at a maximum length
				var more = content.IndexOf("<!--more-->");
				if (more > 0)
				{
					content = content.Substring(0, more).ReplaceHtmlTags(" ");
				}
				else
				{
					content = content.ReplaceHtmlTags(" ").NoLongerThan(maxLength);
				}

				// Return filtered content
				return content.Trim();
			});
	}
}
