﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Create success message
	/// </summary>
	public class CreateMsg : IMsg
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
		/// <param name="id">[Optional] POCO id</param>
		public CreateMsg(Type type, long? id = null)
			=> (this.type, this.id) = (type, id);

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString()
			=> $"Created '{type}'" + (id == null ? "" : "with ID '{id}'") + ".";
	}

	/// <summary>
	/// Message about an error that has occurred during a Create operation
	/// </summary>
	public class CreateErrorMsg : CreateMsg
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		public CreateErrorMsg(Type type) : base(type) { }

		/// <summary>
		/// Output error message
		/// </summary>
		public override string ToString()
			=> $"Unable to Create '{type}'.";
	}

	/// <summary>
	/// Message about an exception that has occurred during a Create operation
	/// </summary>
	public class CreateExceptionMsg : CreateErrorMsg
	{
		private readonly string exceptionMessage;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		/// <param name="type">POCO type</param>
		public CreateExceptionMsg(Exception ex, Type type) : base(type)
			=> exceptionMessage = new ExceptionMsg(ex).ToString();

		/// <summary>
		/// Output error message plus exception details
		/// </summary>
		public override string ToString()
			=> $"{base.ToString()} {exceptionMessage}";
	}
}
