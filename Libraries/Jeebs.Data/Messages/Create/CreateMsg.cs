using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Create success message
	/// </summary>
	public class CreateMsg : LoggableMsg
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
			=> "Created '{Type}' with ID '{Id}'.";

		/// <inheritdoc/>
		public override object[] ParamArray
			=> new object[] { type.ToString(), id };

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public CreateMsg(Type type, long id)
			=> (this.type, this.id) = (type, id);
	}
}
