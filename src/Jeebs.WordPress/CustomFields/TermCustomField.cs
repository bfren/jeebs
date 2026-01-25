// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.CustomFields;

/// <summary>
/// Term Taxonomy Custom Field.
/// </summary>
public abstract class TermCustomField : CustomField<TermCustomField.Term>
{
	/// <summary>
	/// IQueryTerms.
	/// </summary>
	protected IQueryTerms QueryTerms { get; private init; }

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected TermCustomField(string key) : this(new Terms(), key) { }

	/// <summary>
	/// Create object from terms.
	/// </summary>
	/// <param name="queryTerms">IQueryTerms.</param>
	/// <param name="key">Meta key (for post_meta table).</param>
	protected TermCustomField(IQueryTerms queryTerms, string key) : base(key, new Term()) =>
		QueryTerms = queryTerms;

	/// <inheritdoc/>
	public override Task<Result<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
	{
		// First, get the Term ID from the meta dictionary
		// If meta doesn't contain the key and this is a required field, return failure
		// Otherwise return success
		if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
		{
			ValueStr = value;
		}
		else
		{
			if (isRequired)
			{
				return R.Fail("Meta Key '{Key}' not found for Custom Field '{Type}'.", Key, GetType().Name)
					.Ctx(GetType().Name, nameof(HydrateAsync))
					.AsTask<bool>();
			}

			return R.False.AsTask();
		}

		// If we're here we have a Term ID, so get it and hydrate the custom field
		return
			R.Wrap(
				ValueStr
			)
			.Bind(
				x => ParseTermId(GetType(), x)
			)
			.BindAsync(
				x => QueryTerms.ExecuteAsync<Term>(db, w, opt => opt with { Id = x })
			)
			.GetSingleAsync(
				x => x.Value<Term>(),
				(msg, args) => R.Fail("Unable to get single '{ValueStr}': " + msg, [ValueStr, .. args])
					.Ctx(GetType().Name, nameof(HydrateAsync))
			)
			.MapAsync(
				x =>
				{
					ValueObj = x;
					return true;
				}
			);
	}

	/// <summary>
	/// Parse the Term ID.
	/// </summary>
	/// <param name="type">Term Custom Field type.</param>
	/// <param name="value">Term ID value.</param>
	internal static Result<WpTermId> ParseTermId(Type type, string value) =>
		M.ParseUInt64(value).Match(
			some: x => R.Wrap(new WpTermId { Value = x }),
			none: () => R.Fail("'{Value}' is not a valid Term ID.", value)
				.Ctx(type.Name, nameof(ParseTermId))
		);

	/// <summary>
	/// Return Term Title.
	/// </summary>
	protected override string GetValueAsString() =>
		ValueObj.Title;

	internal string GetValueAsStringTest() =>
		GetValueAsString();

	/// <summary>
	/// Term class.
	/// </summary>
	public sealed record class Term : WpTermEntity { }
}
