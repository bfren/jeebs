// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs.WordPress.Data
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
			get =>
				ValueStr ?? string.Empty;

			protected set =>
				ValueStr = value;
		}

		/// <inheritdoc/>
		protected TextCustomField(string key) : base(key, string.Empty) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired)
		{
			// If meta contains the key, return it
			if (meta.TryGetValue(Key, out string? value))
			{
				ValueStr = value;
				return True.AsTask;
			}

			// Return error if the field is required
			if (isRequired)
			{
				return None<bool>(new Msg.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
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
		public static class Msg
		{
			/// <summary>Meta key not found in MetaDictionary</summary>
			/// <param name="Type">Custom Field type</param>
			/// <param name="Value">Meta Key</param>
			public sealed record MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string> { }
		}
	}
}
