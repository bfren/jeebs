// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data
{
	public sealed class WpDbQuery : DbQuery
	{
		private IWpDb WpDb =>
			(IWpDb)Db;

		public WpDbQuery(IWpDb wpDb, ILog log) : base(wpDb, log) { }
	}
}
