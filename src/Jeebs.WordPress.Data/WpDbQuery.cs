// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IWpDbQuery"/>
	internal sealed class WpDbQuery : DbQuery, IWpDbQuery
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="log">ILog</param>
		internal WpDbQuery(IWpDb db, ILog<IWpDbQuery> log) : base(db, log) { }
	}
}
