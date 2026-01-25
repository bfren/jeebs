// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Jeebs.Functions;
using RndF;

namespace Jeebs.WordPress.ContentFilters.Blocks;

/// <summary>
/// Parse embed blocks.
/// </summary>
internal static partial class Embed
{
	/// <summary>
	/// Parse a generic embedded object.
	/// </summary>
	/// <param name="content">Post content.</param>
	/// <param name="type">EmbedType.</param>
	/// <param name="provider">Provider.</param>
	/// <param name="format">Function to parse matched content.</param>
	internal static string Parse(string content, EmbedType type, Provider provider, Func<string, EmbedParsed, string> format)
	{
		// Get Embedded info
		var matches = EmbedRegex().Matches(content);
		if (matches.Count == 0)
		{
			return content;
		}

		// Parse each match
		foreach (var match in matches.Cast<Match>())
		{
			// Info is encoded as JSON so deserialise it first
			var info = match.Groups[1].Value;
			_ = JsonF.Deserialise<EmbedParsed>(info).IfOk(embed =>
			{
				// Only replace matching embed types
				if (embed.Type == type && embed.ProviderNameSlug == provider)
				{
					// Replace content using child Format() method
					content = content.Replace(
						match.Value,
						format(Rnd.StringF.Get(10), embed)
					);
				}
			});
		}

		// Return parsed content
		return content;
	}

	/// <summary>
	/// Used to parse Embed JSON.
	/// </summary>
	/// <param name="Url">Embedded resource URL.</param>
	/// <param name="Type">Resource type.</param>
	/// <param name="ProviderNameSlug">Provider.</param>
	internal sealed record class EmbedParsed(string Url, EmbedType Type, Provider ProviderNameSlug);

	/// <summary>
	/// Supported embed types.
	/// </summary>
	internal enum EmbedType
	{
		Video = 0
	}

	/// <summary>
	/// Supported providers.
	/// </summary>
	internal enum Provider
	{
		Vimeo = 0
	}

	[GeneratedRegex("<!-- wp:embed ({.*?}) -->(.*?)<!-- /wp:embed -->", RegexOptions.Singleline)]
	private static partial Regex EmbedRegex();
}
