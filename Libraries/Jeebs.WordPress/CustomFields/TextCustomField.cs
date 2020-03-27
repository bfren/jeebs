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
		public override string Val => Value;

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
			if (meta.ContainsKey(Key))
			{
				Value = meta[Key];
				return await Task.FromResult(Result.Success());
			}

			return await Task.FromResult(Result.Failure($"Key not found in meta dictionary: '{Key}'."));
		}
	}
}
