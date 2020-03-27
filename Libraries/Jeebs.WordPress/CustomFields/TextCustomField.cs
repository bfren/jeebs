using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Simple text value custom field
	/// </summary>
	public abstract class TextCustomField : CustomField<string>
	{
		/// <summary>
		/// Custom Field value
		/// </summary>
		public override string ValueObj
		{
			get => ValueStr;
			protected set => ValueStr = value;
		}

		/// <summary>
		/// Pass post_meta key to parent
		/// </summary>
		/// <param name="key">Meta key (for post_meta table)</param>
		/// <param name="isRequired">Whether or not this custom field is required</param>
		protected TextCustomField(string key, bool isRequired = false) : base(key, isRequired) { }

		/// <summary>
		/// Hydrate this Field
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="meta">MetaDictionary</param>
		public override async Task<Result> Hydrate(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta doesn't contain the key and this is a required field, return failure
			// Otherwise return success
			if (!meta.ContainsKey(Key))
			{
				if (IsRequired)
				{
					return await Result.FailureAsync($"Key not found in meta dictionary: '{Key}'.");
				}

				return await Result.SuccessAsync();
			}

			// Set value from met and return success
			ValueObj = ValueStr = meta[Key];
			return await Result.SuccessAsync();
		}
	}
}
