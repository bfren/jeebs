// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertGenericArgument{TExpected}(NSubstitute.Core.ICall)"/>
/// </summary>
public sealed class GenericArgumentException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public GenericArgumentException() { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public GenericArgumentException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public GenericArgumentException(string message, Exception inner) : base(message, inner) { }
}
