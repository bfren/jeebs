using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Create success message
	/// </summary>
	public class Create : IMessage
	{
		protected readonly Type type;

		protected readonly long? id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">[Optional] POCO id</param>
		public Create(Type type, long? id = null)
		{
			this.type = type;
			this.id = id;
		}

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString() => $"Created '{type}'" + (id == null ? "" : "with ID '{id}'") + ".";
	}

	/// <summary>
	/// Message about an error that has occurred during a Create operation
	/// </summary>
	public class CreateError : Create
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		public CreateError(Type type) : base(type) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString() => $"Unable to Create '{type}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during a Create operation
	/// </summary>
	public class CreateException : CreateError
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		public CreateException(System.Exception ex, Type type) : base(type)
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
