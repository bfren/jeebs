using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Term Taxonomy Custom Field
	/// </summary>
	public abstract partial class TermCustomField : CustomField<TermCustomField.Term>
	{
		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="key">Meta key</param>
		/// <param name="isRequired">[Optional] Whether or not this custom field is required (default: false)</param>
		protected TermCustomField(string key, bool isRequired = false) : base(key, isRequired) => ValueObj = new Term();

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
					return Result.Failure($"Key not found in meta dictionary: '{Key}'.");
				}

				return Result.Success();
			}

			ValueStr = meta[Key];

			// Get meta value as post ID
			if (!long.TryParse(ValueStr, out var termId))
			{
				return Result.Failure($"'{ValueStr}' is not a valid Term ID.");
			}

			throw new NotImplementedException();
		}
	}
}
