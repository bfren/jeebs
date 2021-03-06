// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Data
{
	/// <summary>
	/// Database entity with version
	/// </summary>
	public interface IEntityWithVersion : IEntity
	{
		/// <summary>
		/// Entity version
		/// </summary>
		long Version { get; set; }
	}

	/// <summary>
	/// Database entity with version
	/// </summary>
	public interface IEntityWithVersion<T> : IEntityWithVersion, IEntity<T>
	{
	}
}
