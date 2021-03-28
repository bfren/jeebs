// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data
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
	/// <typeparam name="T">Strong ID type</typeparam>
	public interface IEntityWithVersion<T> : IEntity<T>
	{
		/// <summary>
		/// Entity version
		/// </summary>
		long Version { get; set; }
	}
}
