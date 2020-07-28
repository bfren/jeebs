﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
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
		/// Whether or not this Custom Field is required (default: false)
		/// </summary>
		bool IsRequired { get; }

		/// <summary>
		/// Hydrate this Custom Field using a combination of <see cref="IWpDb"/>, <see cref="IUnitOfWork"/>, and <see cref="MetaDictionary"/>.
		/// </summary>
		/// <param name="r">Result</param>
		/// <param name="db">IWpDb</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="meta">MetaDictionary</param>
		Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta);
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
