// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress.Data;

/// <summary>
/// Represents a CustomField, which are stored in the post_meta table
/// </summary>
public interface ICustomField
{
	/// <summary>
	/// Custom Field key
	/// </summary>
	string Key { get; }

	/// <summary>
	/// Hydrate this Custom Field using <see cref="IWpDb"/>, and <see cref="MetaDictionary"/>
	/// </summary>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="meta">Meta Dictionary</param>
	/// <param name="isRequired">Whether or not the field is required</param>
	Task<Option<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired);
}

/// <inheritdoc/>
/// <typeparam name="T">Value type</typeparam>
public interface ICustomField<T> : ICustomField
{
	/// <summary>
	/// Custom Field Value
	/// </summary>
	T ValueObj { get; }
}
