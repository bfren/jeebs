// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Delete success message
	/// </summary>
	public class DeleteMsg : LoggableMsg
	{
		/// <summary>
		/// Entity Type
		/// </summary>
		protected readonly Type type;

		/// <summary>
		/// Entity ID
		/// </summary>
		protected readonly long id;

		/// <inheritdoc/>
		public override string Format =>
			"Deleted '{Type}' with ID '{Id}'.";

		/// <inheritdoc/>
		public override object[] ParamArray =>
			new object[] { type.ToString(), id };

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteMsg(Type type, long id) =>
			(this.type, this.id) = (type, id);
	}
}
