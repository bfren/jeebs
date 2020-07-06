using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Update success message
	/// </summary>
	public class Update : IMessage
	{
		protected readonly Type type;

		protected readonly long id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public Update(Type type, long id)
		{
			this.type = type;
			this.id = id;
		}

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString() => $"Updated '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an error that has occurred during an update operation
	/// </summary>
	public class UpdateError : Update
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public UpdateError(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() => $"Unable to update '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during an update operation
	/// </summary>
	public class UpdateException : UpdateError
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public UpdateException(System.Exception ex, Type type, long id) : base(type, id)
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
