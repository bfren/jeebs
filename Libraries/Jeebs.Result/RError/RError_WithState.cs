// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <inheritdoc cref="IError{TValue, TState}"/>
	public class RError<TValue, TState> : R<TValue, TState>, IError<TValue, TState>
	{
		internal RError(TState state) : base(state) { }
	}
}
