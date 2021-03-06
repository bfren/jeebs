// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.WordPress.CustomFields
{
	/// <summary>
	/// ValueStr is an invalid Post ID when hydrating a custom field
	/// </summary>
	public class ValueIsInvalidPostIdMsg : WithValueMsg<string>
	{
		private Type CustomFieldType { get; }

		internal ValueIsInvalidPostIdMsg(Type customFieldType, string value) : base(value) =>
			CustomFieldType = customFieldType;

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString() =>
			$"'{Value}' is not a valid Post ID for Custom Field '{CustomFieldType}'.";
	}
}
