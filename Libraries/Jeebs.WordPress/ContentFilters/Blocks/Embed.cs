using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static F.JsonF;

namespace Jeebs.WordPress.ContentFilters.Blocks
{
	/// <summary>
	/// Embed block
	/// </summary>
	internal abstract class Embed : ParseBlocks.Block
	{
		private readonly EmbedType type;

		private readonly Provider provider;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">EmbedType</param>
		/// <param name="provider">Provider</param>
		protected Embed(EmbedType type, Provider provider) =>
			(this.type, this.provider) = (type, provider);

		/// <summary>
		/// Format a parsed block
		/// </summary>
		/// <param name="id">Block ID</param>
		/// <param name="block">EmbedParsed</param>
		protected abstract string Format(string id, Parsed block);

		/// <summary>
		/// Parse content
		/// </summary>
		/// <param name="content">Content to parse</param>
		internal override string Parse(string content)
		{
			// Get Embedded info
			var matches = Regex.Matches(content, "<!-- wp:embed ({.*?}) -->(.*?)<!-- /wp:embed -->", RegexOptions.Singleline);
			if (matches.Count == 0)
			{
				return content;
			}

			// Parse each match
			foreach (Match match in matches)
			{
				// Info is encoded as JSON
				var json = match.Groups[1].Value;
				if (Deserialise<Parsed>(json) is Some<Parsed> embed)
				{
					// If the embed type and provider do not match, continue
					if (embed.Value.Type != type || embed.Value.ProviderNameSlug != provider)
					{
						continue;
					}

					// Replace content using child Format() method
					content = content.Replace(
						match.Value,
						Format(F.StringF.Random(10), embed.Value)
					);
				}
			}

			// Return parsed content
			return content;
		}

		/// <summary>
		/// Used to parse Embed JSON
		/// </summary>
		protected class Parsed
		{
			/// <summary>
			/// YouTube Url
			/// </summary>
			public string Url { get; set; } = string.Empty;

			/// <summary>
			/// EmbedType
			/// </summary>
			public EmbedType Type { get; set; }

			/// <summary>
			/// Provider
			/// </summary>
			public Provider ProviderNameSlug { get; set; }
		}

		/// <summary>
		/// Supported embed types
		/// </summary>
		protected enum EmbedType
		{
			Video
		}

		/// <summary>
		/// Supported providers
		/// </summary>
		protected enum Provider
		{
			Vimeo
		}
	}
}
