// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;
using Jm.WordPress.CustomFields;
using static JeebsF.OptionF;

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
		public override Task<Option<bool>> HydrateAsync(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta contains the key and the value is not null / empty, return it
			if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
			{
				ValueObj = ValueStr = value;
				return True.AsTask;
			}

			// Return error if the field is required
			if (IsRequired)
			{
				return None<bool>(new MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
			}

			// Return OK but not set
			return False.AsTask;
		}
	}
}
