using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Microsoft.Extensions.Logging;

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
		public override string Format
			=> "Deleted '{Type}' with ID '{Id}'.";

		/// <inheritdoc/>
		public override object[] ParamArray
			=> new object[] { type.FullName, id };

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteMsg(Type type, long id)
			=> (this.type, this.id) = (type, id);
	}
}
