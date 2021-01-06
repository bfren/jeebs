using System;
using System.Collections.Generic;
using System.Text;
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
			get => ValueStr;
			protected set => ValueStr = value;
		}

		/// <inheritdoc/>
		protected TextCustomField(string key, bool isRequired = false) : base(key, string.Empty, isRequired) { }

		/// <inheritdoc/>
		public override async Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta doesn't contain the key and this is a required field, return failure
			// Otherwise return success
			if (meta.ContainsKey(Key))
			{
				ValueObj = ValueStr = meta[Key];
				return r.OkTrue();
			}
			else if (IsRequired)
			{
				return r.Error<bool>().AddMsg(new MetaKeyNotFoundMsg(Key));
			}

			// Return OK but not set
			return r.OkFalse();
		}
	}
}
