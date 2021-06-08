﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="ICustomField{T}"/>
	public abstract class CustomField<T> : ICustomField<T>
	{
		/// <inheritdoc/>
		public string Key { get; private init; }

		/// <inheritdoc/>
		public virtual T ValueObj { get; protected set; }

		/// <summary>
		/// String representation of the value - normally retrieved from the database
		/// </summary>
		protected string? ValueStr { get; set; }

		/// <summary>
		/// Create object with specified meta key
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="value">Default value</param>
		protected CustomField(string key, T value) =>
			(Key, ValueObj) = (key, value);

		/// <inheritdoc/>
		public abstract Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired);

		/// <summary>
		/// Return the value, or post_meta key (instead of the class name)
		/// </summary>
		protected virtual string GetValueAsString() =>
			ValueObj?.ToString() ?? ValueStr ?? Key;

		/// <summary>
		/// Don't use the default ToString()
		/// </summary>
		public override string ToString() =>
			GetValueAsString();
	}
}
