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
		/// <param name="s">Input string</param>
		/// <param name="perform">Function to modify and return the input string</param>
		/// <param name="empty">[Optional] String to return if s is empty</param>
		/// <returns>Modified input string</returns>
		private static string Modify(string s, Func<string> perform, string? empty = null)
		{
			if (string.IsNullOrEmpty(s))
			{
				return empty ?? s;
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
		/// <param name="s">Input string</param>
		/// <param name="ls">Left single quote mark</param>
		/// <param name="rs">Right single quote mark</param>
		/// <param name="ld">Left double quote mark</param>
		/// <param name="rd">Right double quote mark</param>
		/// <returns>Input string with straight quotes converted to curly quotes</returns>
		public static string ConvertCurlyQuotes(this string s, string ls = "‘", string rs = "’", string ld = "“", string rd = "”")
		{
			return Modify(s, () =>
			{
				s = s.Replace("&#34;", "\"");
				s = s.Replace("&#39;", "'");
				s = Regex.Replace(s, "(\\s|^)'", $"$1{ls}").Replace("'", rs);
				return Regex.Replace(s, "(\\s|^)\"", $"$1{ld}").Replace("\"", rd);
			});
		}

		/// <summary>
		/// Ensure that an input string ends with a single defined character
		/// </summary>
		/// <param name="s">The input string</param>
		/// <param name="character">The character to end the string with</param>
		/// <returns>The input string ending with a single 'character'</returns>
		public static string EndWith(this string s, char character)
		{
			return Modify(s, () => string.Format("{0}{1}", s.TrimEnd(character), character));
		}

		/// <summary>
		/// Ensure that an input string ends with another string
		/// </summary>
		/// <param name="s">The input string</param>
		/// <param name="value">The string to end the string with</param>
		/// <returns>The input string ending with string 'value'</returns>
		public static string EndWith(this string s, string value)
		{
			return Modify(s, () => string.Format("{0}{1}", s.TrimEnd(value), value));
		}

		/// <summary>
		/// Escape all single quotes (when outputting text into a Javascript string)
		/// </summary>
		/// <param name="s">String to escape</param>
		/// <returns>Escaped string</returns>
		public static string EscapeSingleQuotes(this string s)
		{
			return Modify(s, () => s.Replace("'", @"\'"));
		}

		/// <summary>
		/// Parse the Mime Type of a filename using its extension
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>Mime Type, or 'application/octet-stream' if it cannot be detected</returns>
		public static string GetMimeFromExtension(this string s)
		{
			return Modify(s, () =>
			{
				// Get the index of the last period
				int lastPeriod = s.LastIndexOf('.');
				if (lastPeriod == -1)
				{
					return s;
				}

				// Get the extension and switch to get the mime type
				string extension = s.Substring(lastPeriod + 1).ToLower();
				switch (extension)
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
		}

		/// <summary>
		/// Ensure a string is no longer than the specified maximum
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="maxLength">The maximum length of the string</param>
		/// <param name="continuation">The continuation string to append to strings longer than the maximum</param>
		/// <param name="empty">Text to return if the primary string is empty</param>
		/// <returns>Modified input string</returns>
		public static string NoLongerThan(this string s, int maxLength, string continuation = "..", string? empty = null)
		{
			return Modify(s, () => (maxLength > 0 && s.Length > maxLength) ? s.Substring(0, maxLength) + continuation : s, empty);
		}

		/// <summary>
		/// Normalise a string by making it lowercase, stripping all non-letters and replacing spaces with -
		/// </summary>
		/// <param name="s">String to perform operation on</param>
		/// <returns>Normalised string</returns>
		public static string Normalise(this string s)
		{
			return Modify(s, () =>
			{
				// Make lowercase, and remove non-letters characters
				string normalised = Regex.Replace(s.ToLower(), "[^a-z -]", "").Trim();

				// Remove hyphens from the start of the string
				normalised = normalised.TrimStart('-');

				// Replace multiple spaces and hyphens with a single hyphen
				return Regex.Replace(normalised, "[ -]+", "-");
			});
		}

		/// <summary>
		/// Return null if the string is empty or null - otherwise, return the string
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String object, or null</returns>
		public static string NullIfEmpty(this string s)
		{
			return Modify(s, () => s);
		}

		/// <summary>
		/// Replace all strings in an array
		/// </summary>
		/// <param name="s">String to perform operation on</param>
		/// <param name="replace">Array of strings to replace</param>
		/// <param name="with">String to replace occurrences with</param>
		/// <returns>String with all strings in the array replaced</returns>
		public static string ReplaceAll(this string s, string[] replace, string with)
		{
			return Modify(s, () =>
			{
				// Copy string and replace values
				string r = s;
				foreach (string t in replace)
				{
					r = r.Replace(t, with);
				}

				return r;
			});
		}

		/// <summary>
		/// Ensure a string contains only numbers
		/// </summary>
		/// <param name="s">The input string</param>
		/// <param name="replaceWith">String to replace non-numerical characters with</param>
		/// <returns>Input string with all non-numerical characters removed</returns>
		public static string ReplaceNonNumerical(this string s, string? replaceWith = null)
		{
			return Modify(s, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all non-numerical characters
				return Regex.Replace(s, "[^0-9]+", replaceWith);
			});
		}

		/// <summary>
		/// Replace non-word characters in a string, useful for creating HTML IDs (for example)
		/// </summary>
		/// <param name="s">String to perform operation on</param>
		/// <param name="replaceWith">String to replace unwanted characters with</param>
		/// <returns>String with unwanted characters replaced</returns>
		public static string ReplaceNonWord(this string s, string? replaceWith = null)
		{
			return Modify(s, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all non-word characters
				return Regex.Replace(s, @"\W+", replaceWith);
			});
		}

		/// <summary>
		/// Split a CamelCase string by capitals
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String split by capital letters</returns>
		public static string SplitByCapitals(this string s)
		{
			return Modify(s, () => Regex.Replace(s, "( *)([A-Z])", " $2").Trim());
		}

		/// <summary>
		/// Ensure that an input string starts with a single defined character
		/// </summary>
		/// <param name="s">The input string</param>
		/// <param name="character">The character to start the string with</param>
		/// <returns>The input string starting with a single 'character'</returns>
		public static string StartWith(this string s, char character)
		{
			return Modify(s, () => string.Format("{0}{1}", character, s.TrimStart(character)));
		}

		/// <summary>
		/// Return the input string encoded into ASCII Html Entities
		/// Warning: this only works with ASCII 'Printable' characters (32->126), NOT 'Extended' characters
		/// </summary>
		/// <param name="s">The input string</param>
		/// <returns>ASCII-encoded String</returns>
		public static string ToASCII(this string s)
		{
			return Modify(s, () =>
			{
				// Get ASCII encoding and convert byte by byte
				byte[] a = Encoding.ASCII.GetBytes(s);

				string encoded = string.Empty;
				foreach (byte b in a)
				{
					encoded += string.Format("&#{0};", b);
				}

				return encoded;
			});
		}

		/// <summary>
		/// Equivalent of PHP lcfirst() - makes the first character of a string lowercase
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String, with the first letter forced to Lowercase</returns>
		public static string ToLowerFirst(this string s)
		{
			return Modify(s, () => char.ToLower(s[0]) + s.Substring(1));
		}

		/// <summary>
		/// Equivalent of PHP ucfirst() - except it lowers the case of all subsequent letters as well
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToSentenceCase(this string s)
		{
			return Modify(s, () => char.ToUpper(s[0]) + s.Substring(1).ToLower());
		}

		/// <summary>
		/// Converts a string to Title Case (ignoring acronyms)
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String converted to Title Case</returns>
		public static string ToTitleCase(this string s)
		{
			return Modify(s, () =>
			{
				char[] array = s.ToCharArray();

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
		}

		/// <summary>
		/// Equivalent of PHP ucfirst() - makes the first character of a string uppercase
		/// </summary>
		/// <param name="s">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToUpperFirst(this string s)
		{
			return Modify(s, () => char.ToUpper(s[0]) + s.Substring(1));
		}

		/// <summary>
		/// Trim a string from
		/// </summary>
		/// <param name="s"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string TrimEnd(this string s, string value)
		{
			if (!s.EndsWith(value))
			{
				return s;
			}

			return s.Remove(s.LastIndexOf(value));
		}

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
			{"(.+(s|x|sh|ch))es$", "$1"},
			{"(.+)s", "$1"},
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
		/// <param name="s">The string to singularise</param>
		/// <returns>Singularised string</returns>
		public static string Singularise(this string s)
		{
			if (unpluralisables.Contains(s.ToLowerInvariant()))
			{
				return s;
			}

			string r = s;
			foreach (var item in singularisations)
			{
				if (Regex.IsMatch(s, item.Key))
				{
					r = Regex.Replace(s, item.Key, item.Value);
				}
			}

			return r;
		}

		/// <summary>
		/// Returns true if the word is plural
		/// </summary>
		/// <param name="s">The word to check</param>
		/// <returns>True if the word is plural</returns>
		public static bool IsPlural(this String s)
		{
			if (unpluralisables.Contains(s.ToLowerInvariant()))
			{
				return true;
			}

			foreach (var item in singularisations)
			{
				if (Regex.IsMatch(s, item.Key))
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
			{ "(.+(s|x|sh|ch))$", "$1es"},
			{ "(.+)", "$1s" }
		};

		/// <summary>
		/// 'Pluralise' a string
		/// </summary>
		/// <param name="s">The string to pluralise</param>
		/// <param name="count">The number of items</param>
		/// <returns>Pluralised string</returns>
		public static string Pluralise(this string s, double count)
		{
			if (count == 1)
			{
				return s;
			}

			if (unpluralisables.Contains(s))
			{
				return s;
			}

			var plural = "unknown";

			foreach (var pluralisation in _pluralisations)
			{
				if (Regex.IsMatch(s, pluralisation.Key))
				{
					plural = Regex.Replace(s, pluralisation.Key, pluralisation.Value);
					break;
				}
			}

			return plural;
		}

		#endregion

		/// <summary>
		/// Replace all HTML tags
		/// </summary>
		/// <param name="s">The input string</param>
		/// <param name="replaceWith">String to replace HTML tags with</param>
		/// <returns>Input string with all HTML tags removed</returns>
		public static string ReplaceHtmlTags(this string s, string? replaceWith = null)
		{
			return Modify(s, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all HTML characters
				Regex re = new Regex("<.*?>");
				return re.Replace(s, replaceWith);
			});
		}
	}
}
