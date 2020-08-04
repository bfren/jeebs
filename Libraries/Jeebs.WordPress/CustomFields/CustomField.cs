using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <inheritdoc cref="ICustomField{T}"/>
	public abstract class CustomField<T> : ICustomField<T>
	{
		/// <inheritdoc/>
		public string Key { get; }

		/// <inheritdoc/>
		public virtual T ValueObj { get; protected set; }

		/// <summary>
		/// String representation of the value - normally retrieved from the database
		/// </summary>
		protected string ValueStr { get; set; }

		/// <inheritdoc/>
		public bool IsRequired { get; }

		/// <summary>
		/// Create object with specified meta key
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="value">Default value</param>
		/// <param name="isRequired">Whether or not this custom field is required</param>
		protected CustomField(string key, T value, bool isRequired = false)
		{
			Key = key;
			ValueObj = value;
			ValueStr = string.Empty;
			IsRequired = isRequired;
		}

		/// <inheritdoc/>
		public abstract Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta);

		/// <summary>
		/// Return the value, or post_meta key (instead of the class name)
		/// </summary>
		public override string ToString()
			=> ValueObj?.ToString() ?? (ValueStr ?? Key);
	}
}
