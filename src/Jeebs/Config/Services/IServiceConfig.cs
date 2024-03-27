// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config;

/// <summary>
/// Service configuration interface.
/// </summary>
public interface IServiceConfig
{
	/// <summary>
	/// Whether or not this service configuration is valid.
	/// </summary>
	bool IsValid { get; }
}
