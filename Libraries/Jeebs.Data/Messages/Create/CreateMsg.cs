using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Microsoft.Extensions.Logging;

namespace Jm.Data
{
	/// <summary>
	/// Create success message
	/// </summary>
	public class CreateMsg : ILoggableMsg
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
		public string Format
			=> $"Created '{{{nameof(type)}}}' with ID '{{{nameof(id)}}}'.";

		/// <inheritdoc/>
		public object[] ParamArray
			=> new object[] { type, id };

		/// <inheritdoc/>
		public LogLevel Level
			=> LogLevel.Debug;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public CreateMsg(Type type, long id)
			=> (this.type, this.id) = (type, id);

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString()
			=> string.Format(Format, ParamArray);
	}
}
