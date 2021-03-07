// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.CustomFields
{
	/// <summary>
	/// Meta key not found when hydrating a custom field
	/// </summary>
	public class MetaKeyNotFoundMsg : WithValueMsg<string>
	{
		private Type CustomFieldType { get; }

		internal MetaKeyNotFoundMsg(Type customFieldType, string metaKey) : base(metaKey) =>
			CustomFieldType = customFieldType;

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString() =>
			$"Key '{Value}' not found in meta dictionary for Custom Field '{CustomFieldType}'.";
	}
}
