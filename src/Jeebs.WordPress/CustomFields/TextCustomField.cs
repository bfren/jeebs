// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress.CustomFields;

/// <summary>
/// Simple text value custom field.
/// </summary>
public abstract class TextCustomField : CustomField<string>
{
	/// <summary>
	/// Custom Field value.
	/// </summary>
	public override string ValueObj
	{
		get =>
			ValueStr ?? string.Empty;

		protected set =>
			ValueStr = value;
	}

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected TextCustomField(string key) : base(key, string.Empty) { }

	/// <inheritdoc/>
	public override Task<Result<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
	{
		// If meta contains the key, return it
		if (meta.TryGetValue(Key, out var value))
		{
			ValueStr = value;
			return R.True.AsTask();
		}

		// Return error if the field is required
		if (isRequired)
		{
			return R.Fail("Meta Key '{Key}' not found for Custom Field '{Type}'.", Key, GetType())
				.Ctx(GetType().Name, nameof(HydrateAsync))
				.AsTask<bool>();
		}

		// Return OK but not set
		return R.False.AsTask();
	}

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj;

	internal string GetValueAsStringTest() =>
		GetValueAsString();
}
