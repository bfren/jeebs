using System;
using System.Collections.Generic;
using System.Text;

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
