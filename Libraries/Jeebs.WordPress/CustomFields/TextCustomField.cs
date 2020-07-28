using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jm.WordPress.CustomField.Hydrate;

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
		protected TextCustomField(string key, bool isRequired = false) : base(key, isRequired) { }

		/// <inheritdoc/>
		public override async Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta doesn't contain the key and this is a required field, return failure
			// Otherwise return success
			if (!meta.ContainsKey(Key))
			{
				if (IsRequired)
				{
					return r.False().AddMsg(new MetaKeyNotFoundMsg(Key));
				}

				return r.True();
			}

			// Set value from met and return success
			ValueObj = ValueStr = meta[Key];
			return r.True();
		}
	}
}
