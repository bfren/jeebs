// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;
using static F.JsonF;

namespace Jeebs.WordPress.Data.ContentFilters;

public sealed partial class ParseBlocks
{
	/// <summary>
	/// Parse a generic embedded object
	/// </summary>
	/// <param name="content">Post content</param>
	/// <param name="type">EmbedType</param>
	/// <param name="provider">Provider</param>
	/// <param name="format">Function to parse matched content</param>
	internal static string ParseEmbed(string content, EmbedType type, Provider provider, Func<string, EmbedParsed, string> format)
	{
		// Get Embedded info
		const string pattern = "<!-- wp:embed ({.*?}) -->(.*?)<!-- /wp:embed -->";
		var matches = Regex.Matches(content, pattern, RegexOptions.Singleline);
		if (matches.Count == 0)
		{
			return content;
		}

		// Parse each match
		foreach (Match match in matches)
		{
			// Info is encoded as JSON so deserialise it first
			var info = match.Groups[1].Value;
			Deserialise<EmbedParsed>(info).IfSome(embed =>
			{
				// Only replace matching embed types
				if (embed.Type == type && embed.ProviderNameSlug == provider)
				{
					// Replace content using child Format() method
					content = content.Replace(
						match.Value,
						format(F.Rnd.StringF.Get(10), embed)
					);
				}
			});
		}

		// Return parsed content
		return content;
	}

	/// <summary>
	/// Used to parse Embed JSON
	/// </summary>
	/// <param name="Url">Embedded resource URL</param>
	/// <param name="Type">Resource type</param>
	/// <param name="ProviderNameSlug">Provider</param>
	internal sealed record class EmbedParsed(string Url, EmbedType Type, Provider ProviderNameSlug);

	/// <summary>
	/// Supported embed types
	/// </summary>
	internal enum EmbedType
	{
		Video
	}

	/// <summary>
	/// Supported providers
	/// </summary>
	internal enum Provider
	{
		Vimeo
	}
}
