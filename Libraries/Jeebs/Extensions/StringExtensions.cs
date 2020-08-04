using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jeebs
{
	/// <summary>
	/// String Extensions
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Return empty if the input string is null or empty
		/// </summary>
		/// <param name="this">Input string</param>
		/// <param name="perform">Function to modify and return the input string</param>
		/// <param name="empty">[Optional] String to return if @this is empty</param>
		/// <returns>Modified input string</returns>
		private static string Modify(string @this, Func<string> perform, string? empty = null)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return empty ?? @this;
			}

			if (perform == null)
			{
				throw new ArgumentNullException(nameof(perform));
			}

			return perform();
		}

		/// <summary>
		/// Convert straight quotes to curly quotes
		/// </summary>
		/// <param name="this">Input string</param>
		/// <param name="ls">Left single quote mark</param>
		/// <param name="rs">Right single quote mark</param>
		/// <param name="ld">Left double quote mark</param>
		/// <param name="rd">Right double quote mark</param>
		/// <returns>Input string with straight quotes converted to curly quotes</returns>
		public static string ConvertCurlyQuotes(this string @this, string ls = "‘", string rs = "’", string ld = "“", string rd = "”")
			=> Modify(@this, () =>
			{
				var s = @this.Replace("&#34;", "\"");
				s = s.Replace("&#39;", "'");
				s = Regex.Replace(s, "(\\s|^)'", $"$1{ls}").Replace("'", rs);
				return Regex.Replace(s, "(\\s|^)\"", $"$1{ld}").Replace("\"", rd);
			});

		/// <summary>
		/// Convert straight quotes to curly quotes, but not inside HTML tags / attributes
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>IHtmlContent with curly quotes</returns>
		public static string ConvertInnerHtmlQuotes(this string @this)
			=> Modify(@this, () =>
			{
				// Will hold the result
				string result = string.Empty;

				// Convert using HTML entities
				static string convert(string input)
				{
					return input.ConvertCurlyQuotes(ls: "&lsquo;", rs: "&rsquo;", ld: "&ldquo;", rd: "&rdquo;");
				}

				// Match HTML tags and their attributes
				var htmlTags = new Regex("<[^>]*>");

				// Get the first match
				Match match = htmlTags.Match(@this);

				// Loop through each HTML tag, replacing the quotes in the text in between
				int lastIndex = 0;
				while (match.Success)
				{
					// Replace text between this match and the previous one
					string textBetween = @this[lastIndex..match.Index];
					result += convert(textBetween);

					// Add the text in this section unchanged
					result += match.Value;

					// Move to the next section
					lastIndex = match.Index + match.Length;
					match = match.NextMatch();
				}

				// Replace any remaining quotes
				string remaining = @this[lastIndex..];
				result += convert(remaining);

				// Return result string
				return result;
			});

		/// <summary>
		/// Ensure that an input string ends with a single defined character
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="character">The character to end the string with</param>
		/// <returns>The input string ending with a single 'character'</returns>
		public static string EndWith(this string @this, char character)
			=> Modify(@this, () => string.Format("{0}{1}", @this.TrimEnd(character), character));

		/// <summary>
		/// Ensure that an input string ends with another string
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="value">The string to end the string with</param>
		/// <returns>The input string ending with string 'value'</returns>
		public static string EndWith(this string @this, string value)
			=> Modify(@this, () => string.Format("{0}{1}", @this.TrimEnd(value), value));

		/// <summary>
		/// Escape all single quotes (when outputting text into a Javascript string)
		/// </summary>
		/// <param name="this">String to escape</param>
		/// <returns>Escaped string</returns>
		public static string EscapeSingleQuotes(this string @this)
			=> Modify(@this, () => @this.Replace("'", @"\'"));

		/// <summary>
		/// Parse the Mime Type of a filename using its extension
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>Mime Type, or 'application/octet-stream' if it cannot be detected</returns>
		public static string GetMimeFromExtension(this string @this)
			=> Modify(@this, () =>
			{
				// Get the index of the last period
				int lastPeriod = @this.LastIndexOf('.');
				if (lastPeriod == -1)
				{
					return @this;
				}

				// Get the extension and switch to get the mime type
				switch (@this.Substring(lastPeriod + 1).ToLower())
				{
					case "bmp":
						return MimeType.Bmp.ToString();

					case "doc":
						return MimeType.Doc.ToString();

					case "docx":
						return MimeType.Docx.ToString();

					case "gif":
						return MimeType.Gif.ToString();

					case "jpg":
					case "jpeg":
						return MimeType.Jpg.ToString();

					case "m4a":
						return MimeType.M4a.ToString();

					case "mp3":
						return MimeType.Mp3.ToString();

					case "pdf":
						return MimeType.Pdf.ToString();

					case "png":
						return MimeType.Png.ToString();

					case "ppt":
						return MimeType.Ppt.ToString();

					case "pptx":
						return MimeType.Pptx.ToString();

					case "rar":
						return MimeType.Rar.ToString();

					case "tar":
						return MimeType.Tar.ToString();

					case "txt":
						return MimeType.Text.ToString();

					case "xls":
						return MimeType.Xls.ToString();

					case "xlsx":
						return MimeType.Xlsx.ToString();

					case "zip":
						return MimeType.Zip.ToString();

					default:
						return MimeType.General.ToString();
				}
			});

		/// <summary>
		/// Decode HTML entities
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>String with HTML entities decoded</returns>
		public static string HtmlDecode(this string @this)
			=> Modify(@this, () => System.Net.WebUtility.HtmlDecode(@this));

		/// <summary>
		/// Encode HTML entities
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>String with HTML entities encoded</returns>
		public static string HtmlEncode(this string @this)
			=> Modify(@this, () => System.Net.WebUtility.HtmlEncode(@this));

		/// <summary>
		/// Ensure a string is no longer than the specified maximum
		/// </summary>
		/// <param name="this">Input string</param>
		/// <param name="maxLength">The maximum length of the string</param>
		/// <param name="continuation">The continuation string to append to strings longer than the maximum</param>
		/// <param name="empty">Text to return if the primary string is empty</param>
		/// <returns>Modified input string</returns>
		public static string NoLongerThan(this string @this, int maxLength, string continuation = "..", string? empty = null)
			=> Modify(@this, () => (maxLength > 0 && @this.Length > maxLength) ? @this.Substring(0, maxLength) + continuation : @this, empty);

		/// <summary>
		/// Normalise a string by making it lowercase, stripping all non-letters and replacing spaces with -
		/// </summary>
		/// <param name="this">String to perform operation on</param>
		/// <returns>Normalised string</returns>
		public static string Normalise(this string @this)
			=> Modify(@this, () =>
			{
				// Make lowercase, and remove non-letters characters
				string normalised = Regex.Replace(@this.ToLower(), "[^a-z -]", "").Trim();

				// Remove hyphens from the start of the string
				normalised = normalised.TrimStart('-');

				// Replace multiple spaces and hyphens with a single hyphen
				return Regex.Replace(normalised, "[ -]+", "-");
			});

		/// <summary>
		/// Return null if the string is empty or null - otherwise, return the string
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String object, or null</returns>
		public static string NullIfEmpty(this string @this)
			=> Modify(@this, () => @this);

		/// <summary>
		/// Replace all strings in an array
		/// </summary>
		/// <param name="this">String to perform operation on</param>
		/// <param name="replace">Array of strings to replace</param>
		/// <param name="with">String to replace occurrences with</param>
		/// <returns>String with all strings in the array replaced</returns>
		public static string ReplaceAll(this string @this, string[] replace, string with)
			=> Modify(@this, () =>
			{
				// Copy string and replace values
				string r = @this;
				foreach (string t in replace)
				{
					r = r.Replace(t, with);
				}

				return r;
			});

		/// <summary>
		/// Ensure a string contains only numbers
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="replaceWith">String to replace non-numerical characters with</param>
		/// <returns>Input string with all non-numerical characters removed</returns>
		public static string ReplaceNonNumerical(this string @this, string? replaceWith = null)
			=> Modify(@this, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all non-numerical characters
				return Regex.Replace(@this, "[^0-9]+", replaceWith);
			});

		/// <summary>
		/// Replace non-word characters in a string, useful for creating HTML IDs (for example)
		/// </summary>
		/// <param name="this">String to perform operation on</param>
		/// <param name="replaceWith">String to replace unwanted characters with</param>
		/// <returns>String with unwanted characters replaced</returns>
		public static string ReplaceNonWord(this string @this, string? replaceWith = null)
			=> Modify(@this, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all non-word characters
				return Regex.Replace(@this, @"\W+", replaceWith);
			});

		/// <summary>
		/// Split a CamelCase string by capitals
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String split by capital letters</returns>
		public static string SplitByCapitals(this string @this)
			=> Modify(@this, () => Regex.Replace(@this, "( *)([A-Z])", " $2").Trim());

		/// <summary>
		/// Ensure that an input string starts with a single defined character
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="character">The character to start the string with</param>
		/// <returns>The input string starting with a single 'character'</returns>
		public static string StartWith(this string @this, char character)
			=> Modify(@this, () => string.Format("{0}{1}", character, @this.TrimStart(character)));

		/// <summary>
		/// Return the input string encoded into ASCII Html Entities
		/// Warning: this only works with ASCII 'Printable' characters (32->126), NOT 'Extended' characters
		/// </summary>
		/// <param name="this">The input string</param>
		/// <returns>ASCII-encoded String</returns>
		public static string ToASCII(this string @this)
			=> Modify(@this, () =>
			 {
				 // Get ASCII encoding and convert byte by byte
				 byte[] a = Encoding.ASCII.GetBytes(@this);

				 string encoded = string.Empty;
				 foreach (byte b in a)
				 {
					 encoded += string.Format("&#{0};", b);
				 }

				 return encoded;
			 });

		/// <summary>
		/// Equivalent of PHP lcfirst() - makes the first character of a string lowercase
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Lowercase</returns>
		public static string ToLowerFirst(this string @this)
			=> Modify(@this, () => char.ToLower(@this[0]) + @this.Substring(1));

		/// <summary>
		/// Equivalent of PHP ucfirst() - except it lowers the case of all subsequent letters as well
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToSentenceCase(this string @this)
			=> Modify(@this, () => char.ToUpper(@this[0]) + @this.Substring(1).ToLower());

		/// <summary>
		/// Converts a string to Title Case (ignoring acronyms)
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String converted to Title Case</returns>
		public static string ToTitleCase(this string @this)
			=> Modify(@this, () =>
			{
				char[] array = @this.ToCharArray();

				// Handle the first letter in the string.
				if (array.Length >= 1)
				{
					if (char.IsLower(array[0]))
					{
						array[0] = char.ToUpper(array[0]);
					}
				}

				// Scan through the letters, checking for spaces.
				// ... Uppercase the lowercase letters following spaces.
				for (int i = 1; i < array.Length; i++)
				{
					if (array[i - 1] == ' ')
					{
						if (char.IsLower(array[i]))
						{
							array[i] = char.ToUpper(array[i]);
						}
					}
				}

				return new string(array);
			});

		/// <summary>
		/// Equivalent of PHP ucfirst() - makes the first character of a string uppercase
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToUpperFirst(this string @this)
			=> Modify(@this, () => char.ToUpper(@this[0]) + @this.Substring(1));

		/// <summary>
		/// Trim a string from the end of another string
		/// </summary>
		/// <param name="this">String object</param>
		/// <param name="value">Value to trim</param>
		/// <returns>String, with <paramref name="value"/> trimmed from the end</returns>
		public static string TrimEnd(this string @this, string value)
			=> @this.EndsWith(value) ? @this.Remove(@this.LastIndexOf(value)) : @this;

		// This comes from http://lotsacode.wordpress.com/2010/03/05/singularization-pluralization-in-c/
		#region Singularise

		/// <summary>
		/// Words that cannot be pluralised
		/// </summary>
		private static readonly IList<string> unpluralisables = new List<string>
		{
			"equipment",
			"information",
			"rice",
			"money",
			"species",
			"series",
			"fish",
			"sheep",
			"deer"
		};

		/// <summary>
		/// Rules for converting plural words into singular words
		/// </summary>
		private static readonly IDictionary<string, string> singularisations = new Dictionary<string, string>
		{
			// Start with the rarest cases, and move to the most common
			{"people", "person"},
			{"oxen", "ox"},
			{"children", "child"},
			{"feet", "foot"},
			{"teeth", "tooth"},
			{"geese", "goose"},
			// E.g. wolf, wife
			{"(.*)ives?", "$1ife"},
			{"(.*)ves?", "$1f"},
			// Matrix, index etc
			{"matrices", "matrix"},
			{"indices", "index"},
			{"(.+[^aeiou])ices$","$1ice"},
			{"(.*)ices", "$1ex"},
			// Octopus, virus etc
			{"(octop|vir)i$", "$1us"},
			{"(.+(@this|x|sh|ch))es$", "$1"},
			{"(.+)@this", "$1"},
			// And now the more standard rules.
			{"(.*)men$", "$1man"},
			{"(.+[aeiou])ys$", "$1y"},
			{"(.+[^aeiou])ies$", "$1y"},
			{"(.+)zes$", "$1"},
			{"([m|l])ice$", "$1ouse"}
		};

		/// <summary>
		/// 'Singularise' a string
		/// </summary>
		/// <param name="this">The string to singularise</param>
		/// <returns>Singularised string</returns>
		public static string Singularise(this string @this)
		{
			if (unpluralisables.Contains(@this.ToLowerInvariant()))
			{
				return @this;
			}

			string r = @this;
			foreach (var item in singularisations)
			{
				if (Regex.IsMatch(@this, item.Key))
				{
					r = Regex.Replace(@this, item.Key, item.Value);
				}
			}

			return r;
		}

		/// <summary>
		/// Returns true if the word is plural
		/// </summary>
		/// <param name="this">The word to check</param>
		/// <returns>True if the word is plural</returns>
		public static bool IsPlural(this String @this)
		{
			if (unpluralisables.Contains(@this.ToLowerInvariant()))
			{
				return true;
			}

			foreach (var item in singularisations)
			{
				if (Regex.IsMatch(@this, item.Key))
				{
					return true;
				}
			}

			return false;
		}

		#endregion

		// This comes from http://mattgrande.wordpress.com/2009/10/28/pluralization-helper-for-c/
		#region Pluralise

		/// <summary>
		/// Rules for converting singular words into plural words
		/// </summary>
		private static readonly IDictionary<string, string> _pluralisations = new Dictionary<string, string>
		{
			// Start with the rarest cases, and move to the most common
			{ "person", "people" },
			{ "ox", "oxen" },
			{ "child", "children" },
			{ "foot", "feet" },
			{ "tooth", "teeth" },
			{ "goose", "geese" },
			// And now the more standard rules.
			{ "(.*)fe?$", "$1ves" },         // ie, wolf, wife
			{ "(.*)man$", "$1men" },
			{ "(.+[aeiou]y)$", "$1s" },
			{ "(.+[^aeiou])y$", "$1ies" },
			{ "(.+z)$", "$1zes" },
			{ "([m|l])ouse$", "$1ice" },
			{ "(.+)(e|i)x$", "$1ices"},    // ie, Matrix, Index
			{ "(.+(@this|x|sh|ch))$", "$1es"},
			{ "(.+)", "$1s" }
		};

		/// <summary>
		/// 'Pluralise' a string
		/// </summary>
		/// <param name="this">The string to pluralise</param>
		/// <param name="count">The number of items</param>
		/// <returns>Pluralised string</returns>
		public static string Pluralise(this string @this, double count)
		{
			if (count == 1)
			{
				return @this;
			}

			if (unpluralisables.Contains(@this))
			{
				return @this;
			}

			var plural = "unknown";

			foreach (var pluralisation in _pluralisations)
			{
				if (Regex.IsMatch(@this, pluralisation.Key))
				{
					plural = Regex.Replace(@this, pluralisation.Key, pluralisation.Value);
					break;
				}
			}

			return plural;
		}

		#endregion

		/// <summary>
		/// Replace all HTML tags
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="replaceWith">String to replace HTML tags with</param>
		/// <returns>Input string with all HTML tags removed</returns>
		public static string ReplaceHtmlTags(this string @this, string? replaceWith = null)
			=> Modify(@this, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all HTML characters
				Regex re = new Regex("<.*?>");
				return re.Replace(@this, replaceWith);
			});
	}
}
