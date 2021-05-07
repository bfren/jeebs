// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs.WordPress.Data
{
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
		/// <param name="meta">Meta Dictionary</param>
		/// <param name="isRequired">Whether or not the field is required</param>
		Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired);
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
}
