// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Object (Entity or Model) with Version property
	/// </summary>
	public interface IWithVersion
	{
		/// <summary>
		/// Version
		/// </summary>
		long Version { get; }
	}
}
