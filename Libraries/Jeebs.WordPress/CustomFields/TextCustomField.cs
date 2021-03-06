// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Threading.Tasks;
using Jeebs.Data;
using Jm.WordPress.CustomFields;

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
			get =>
				ValueStr;

			protected set =>
				ValueStr = value;
		}

		/// <inheritdoc/>
		protected TextCustomField(string key, bool isRequired = false) : base(key, string.Empty, isRequired) { }

		/// <inheritdoc/>
		public override async Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta contains the key and the value is not null / empty, return it
			if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
			{
				ValueObj = ValueStr = value;
				return r.OkTrue();
			}

			// Return error if the field is required
			if (IsRequired)
			{
				return r.Error<bool>().AddMsg(new MetaKeyNotFoundMsg(GetType(), Key));
			}

			// Return OK but not set
			return r.OkFalse();
		}
	}
}
