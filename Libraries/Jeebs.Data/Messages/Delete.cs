using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Delete success message
	/// </summary>
	public class DeleteMsg : IMsg
	{
		/// <summary>
		/// Entity Type
		/// </summary>
		protected readonly Type type;

		/// <summary>
		/// Entity ID
		/// </summary>
		protected readonly long? id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteMsg(Type type, long id)
			=> (this.type, this.id) = (type, id);

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString()
			=> $"Deleted '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an error that has occurred during a Delete operation
	/// </summary>
	public class DeleteErrorMsg : DeleteMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteErrorMsg(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString()
			=> $"Unable to Delete '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during a Delete operation
	/// </summary>
	public class DeleteExceptionMsg : DeleteErrorMsg
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteExceptionMsg(Exception ex, Type type, long id) : base(type, id)
			=> exceptionMessage = new ExceptionMsg(ex).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString()
			=> $"{base.ToString()} {exceptionMessage}";
	}
}
