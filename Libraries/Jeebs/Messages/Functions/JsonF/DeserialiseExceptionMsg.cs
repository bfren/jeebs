// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Functions.JsonF
{
	/// <summary>
	/// See <see cref="F.JsonF.Deserialise{T}(string, System.Text.Json.JsonSerializerOptions?)"/>
	/// </summary>
	public sealed class DeserialiseExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public DeserialiseExceptionMsg(Exception e) : base(e) { }
	}
}
