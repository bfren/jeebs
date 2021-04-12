// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IWpDbQuery"/>
	public sealed class WpDbQuery : DbQuery, IWpDbQuery
	{
		/// <inheritdoc/>
		new public IWpDb Db =>
			(IWpDb)base.Db;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="log">ILog</param>
		public WpDbQuery(IWpDb db, ILog<WpDbQuery> log) : base(db, log) { }
	}
}
