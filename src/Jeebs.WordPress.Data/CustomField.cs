// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="ICustomField{T}"/>
	public abstract record CustomField<T> : ICustomField<T>
	{
		/// <inheritdoc/>
		public string Key { get; private init; }

		/// <inheritdoc/>
		public virtual T ValueObj { get; protected set; }

		/// <summary>
		/// String representation of the value - normally retrieved from the database
		/// </summary>
		protected string ValueStr { get; set; }

		/// <summary>
		/// Create object with specified meta key
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="value">Default value</param>
		public CustomField(string key, T value) =>
			(Key, ValueObj, ValueStr) = (key, value, string.Empty);

		/// <inheritdoc/>
		public abstract Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired);

		/// <summary>
		/// Return the value, or post_meta key (instead of the class name)
		/// </summary>
		public override string ToString() =>
			ValueObj?.ToString() ?? ValueStr ?? Key;
	}
}
