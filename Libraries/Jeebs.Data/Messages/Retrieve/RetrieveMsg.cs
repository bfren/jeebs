// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Logging;

namespace Jm.Data
{
	/// <summary>
	/// Retrieve success message
	/// </summary>
	public class RetrieveMsg : LoggableMsg
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
			"Retrieved '{Type}' with ID '{Id}'.";

		/// <inheritdoc/>
		public override object[] ParamArray =>
			new object[] { type.ToString(), id };

		/// <inheritdoc/>
		public override LogLevel Level =>
			LogLevel.Verbose;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveMsg(Type type, long id) =>
			(this.type, this.id) = (type, id);
	}
}
