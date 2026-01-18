// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.WordPress;
using Jeebs.Data;

namespace Jeebs.WordPress;

/// <summary>
/// WordPress Database instance.
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
