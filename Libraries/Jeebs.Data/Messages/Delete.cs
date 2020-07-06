using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Delete success message
	/// </summary>
	public class Delete : IMessage
	{
		protected readonly Type type;

		protected readonly long id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public Delete(Type type, long id)
		{
			this.type = type;
			this.id = id;
		}

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString() => $"Deleted '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an error that has occurred during a Delete operation
	/// </summary>
	public class DeleteError : Delete
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteError(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() => $"Unable to Delete '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during a Delete operation
	/// </summary>
	public class DeleteException : DeleteError
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteException(System.Exception ex, Type type, long id) : base(type, id)
		{
			var message = new Exception(ex);
			exceptionMessage = message.ToString();
		}

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString() => $"{base.ToString()} {exceptionMessage}";
	}
}
