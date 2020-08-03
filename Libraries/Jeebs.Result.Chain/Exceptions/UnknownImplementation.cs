using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jx.Result
{
	/// <summary>
	/// Thrown when a custom implementation of <see cref="IR"/> is used.
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	public class UnknownImplementationException : Exception
	{
		private readonly static string error = $"Unknown implementation of {typeof(IR)}.";

		public UnknownImplementationException() : base(error) { }
		public UnknownImplementationException(string message) : base(message) { }
		public UnknownImplementationException(string message, Exception inner) : base(message, inner) { }
	}
}
