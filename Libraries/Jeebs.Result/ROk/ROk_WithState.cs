using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOk{TValue, TState}"/>
	public class ROk<TResult, TState> : R<TResult, TState>, IOk<TResult, TState>
	{
		internal ROk(TState state) : base(state) { }

		/// <inheritdoc cref="IOk{TValue, TState}.Ok"/>
		public IOk<TResult, TState> Ok() => Ok<TResult>();

		/// <inheritdoc cref="IOk{TValue, TState}.Ok{TNext}"/>
		public IOk<TNext, TState> Ok<TNext>() => this switch
		{
			IOk<TNext, TState> ok => ok,
			_ => new ROk<TNext, TState>(State) { Messages = Messages }
		};

		/// <inheritdoc cref="IOk{TValue, TState}.OkV{TNext}(TNext)"/>
		public IOkV<TNext, TState> OkV<TNext>(TNext value) => new ROkV<TNext, TState>(value, State) { Messages = Messages };

		#region Explicit implementations

		IOk IOk.Ok() => Ok();

		IOk<TResult> IOk<TResult>.Ok() => Ok();

		IOk<TNext> IOk.Ok<TNext>() => Ok<TNext>();

		IOkV<TValue> IOk.OkV<TValue>(TValue value) => OkV(value);

		#endregion
	}
}
