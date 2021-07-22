// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryAttachmentsOptions"/>
		public sealed record AttachmentsOptions : Querying.AttachmentsOptions;
	}
}
