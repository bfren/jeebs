using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Custom Field
	/// </summary>
	public interface ICustomField
	{
		/// <summary>
		/// Custom Field key
		/// </summary>
		string Key { get; }

		/// <summary>
		/// Whether or not this Custom Field is required (default: false)
		/// </summary>
		bool IsRequired { get; }

		/// <summary>
		/// Hydrate this Field
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="meta">MetaDictionary</param>
		Task<Result> Hydrate(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta);
	}

	/// <summary>
	/// Custom Field
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public interface ICustomField<T> : ICustomField
	{
		/// <summary>
		/// Custom Field Value
		/// </summary>
		T ValueObj { get; }
	}
}
