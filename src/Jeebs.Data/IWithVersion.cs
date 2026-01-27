// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data;

/// <summary>
/// Object (Entity or Model) with Version property.
/// </summary>
public interface IWithVersion : IWithId
{
	/// <summary>
	/// Version.
	/// </summary>
	long Version { get; }
}
