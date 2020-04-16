using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Term Taxonomy Custom Field
	/// </summary>
	public abstract class TermCustomField : CustomField<TermCustomField.Term>
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

			// Start a new query
			using var w = db.QueryWrapper;
			var query = w.QueryTaxonomy<Term>(opt => opt.Id = termId);

			// Get results
			var result = await query.ExecuteQuery();
			if (result.Err is ErrorList)
			{
				return Result.Failure(result.Err);
			}

			// Get term
			ValueObj = result.Val.Single();

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Return term Title
		/// </summary>
		public override string ToString() => ValueObj?.Title ?? base.ToString();

		/// <summary>
		/// Term class
		/// </summary>
		public sealed class Term : WpTermEntity { }
	}
}
