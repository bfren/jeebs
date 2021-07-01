// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.Data;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database instance
	/// </summary>
	public interface IWpDb : IDb
	{
		/// <summary>
		/// WordPress configuration
		/// </summary>
		WpConfig WpConfig { get; }

		/// <inheritdoc cref="IWpDbQuery"/>
		IWpDbQuery Query { get; }

		/// <inheritdoc cref="IWpDbSchema"/>
		IWpDbSchema Schema { get; }
	}
}
