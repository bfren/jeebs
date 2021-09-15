// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.TypeHandlers;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.TypeHandlers;

/// <summary>
/// Taxonomy TypeHandler
/// </summary>
public sealed class TaxonomyTypeHandler : EnumeratedTypeHandler<Taxonomy>
{
	/// <summary>
	/// Parse the Taxonomy value
	/// </summary>
	/// <param name="value">Database table value</param>
	/// <returns>Taxonomy object</returns>
	public override Taxonomy Parse(object value) =>
		Parse(value, Taxonomy.Parse, Taxonomy.Blank);
}
