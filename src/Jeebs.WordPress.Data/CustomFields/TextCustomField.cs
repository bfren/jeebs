// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data;
using static F.MaybeF;

namespace Jeebs.WordPress.Data;

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
		get =>
			ValueStr ?? string.Empty;

		protected set =>
			ValueStr = value;
	}

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected TextCustomField(string key) : base(key, string.Empty) { }

	/// <inheritdoc/>
	public override Task<Maybe<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
	{
		// If meta contains the key, return it
		if (meta.TryGetValue(Key, out var value))
		{
			ValueStr = value;
			return True.AsTask;
		}

		// Return error if the field is required
		if (isRequired)
		{
			return None<bool>(new M.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
		}

		// Return OK but not set
		return False.AsTask;
	}

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj;

	internal string GetValueAsStringTest() =>
		GetValueAsString();

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Meta key not found in MetaDictionary</summary>
		/// <param name="Type">Custom Field type</param>
		/// <param name="Value">Meta Key</param>
		public sealed record class MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string> { }
	}
}
