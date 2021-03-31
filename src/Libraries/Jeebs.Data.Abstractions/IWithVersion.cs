// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
