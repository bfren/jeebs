using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Represents a CustomField, which are stored in the post_meta table
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public abstract class CustomField<T> : ICustomField<T>
	{
		/// <summary>
		/// Meta key (for post_meta table)
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Custom Field value
		/// </summary>
		public virtual T ValueObj { get; protected set; }

		/// <summary>
		/// String representation of the value - normally retrieved from the database
		/// </summary>
		protected string ValueStr { get; set; }

		/// <summary>
		/// Whether or not this Custom Field is required (default: false)
		/// </summary>
		public bool IsRequired { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="isRequired">Whether or not this custom field is required</param>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		protected CustomField(string key, bool isRequired = false)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			Key = key;
			ValueStr = string.Empty;
			IsRequired = isRequired;
		}

		/// <summary>
		/// Hydrate this Field
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="meta">MetaDictionary</param>
		public abstract Task<IResult<bool>> HydrateAsync(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta);

		/// <summary>
		/// Return the value, or post_meta key (instead of the class name)
		/// </summary>
		public override string ToString() => ValueObj?.ToString() ?? (ValueStr ?? Key);
	}
}
