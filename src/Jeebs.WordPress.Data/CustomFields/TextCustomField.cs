// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Simple text value custom field
	/// </summary>
	public abstract record TextCustomField : CustomField<string>
	{
		/// <summary>
		/// Custom Field value
		/// </summary>
		public override string ValueObj
		{
			get =>
				ValueStr ?? Key;

			protected set =>
				ValueStr = value;
		}

		/// <inheritdoc/>
		protected TextCustomField(string key) : base(key, string.Empty) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired)
		{
			// If meta contains the key and the value is not null / empty, return it
			if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
			{
				ValueObj = ValueStr = value;
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
