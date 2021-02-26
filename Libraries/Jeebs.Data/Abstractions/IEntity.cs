using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database entity
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Primary key
		/// </summary>
		long Id { get; }
	}

	/// <summary>
	/// Database entity
	/// </summary>
	public interface IEntity<T>
	{
		/// <summary>
		/// Primary key
		/// </summary>
		IStrongId<T> Id { get; init; }
	}
}
