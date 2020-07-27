using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Retrieve success message
	/// </summary>
	public class RetrieveMsg : IMsg
	{
		protected readonly Type type;

		protected readonly long id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveMsg(Type type, long id)
		{
			this.type = type;
			this.id = id;
		}

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString() => $"Retrieved '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an error that has occurred during a Retrieve operation
	/// </summary>
	public class RetrieveErrorMsg : RetrieveMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveErrorMsg(Type type, long id) : base(type, id) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() => $"Unable to Retrieve '{type}' with ID '{id}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during a Retrieve operation
	/// </summary>
	public class RetrieveExceptionMsg : RetrieveErrorMsg
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public RetrieveExceptionMsg(Exception ex, Type type, long id) : base(type, id)
		{
			var message = new ExceptionMsg(ex);
			exceptionMessage = message.ToString();
		}

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString() => $"{base.ToString()} {exceptionMessage}";
	}
}
