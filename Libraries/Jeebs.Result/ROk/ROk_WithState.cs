using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOk{TValue, TState}"/>
	public class ROk<TValue, TState> : R<TValue, TState>, IOk<TValue, TState>
	{
		internal ROk(TState state) : base(state) { }

		/// <inheritdoc cref="IOk{TValue, TState}.Ok"/>
		public IOk<TValue, TState> Ok()
			=> Ok<TValue>();

		/// <inheritdoc cref="IOk{TValue, TState}.Ok{TNext}"/>
		public IOk<TNext, TState> Ok<TNext>()
			=> this switch
			{
				IOk<TNext, TState> ok => ok,
				_ => new ROk<TNext, TState>(State) { Messages = Messages, Logger = Logger }
			};

		/// <inheritdoc cref="IOk{TValue, TState}.OkV{TNext}(TNext)"/>
		public IOkV<TNext, TState> OkV<TNext>(TNext value)
			=> new ROkV<TNext, TState>(value, State) { Messages = Messages, Logger = Logger };

		/// <inheritdoc/>
		public IOk<bool, TState> True(IMsg? message = null)
		{
			if (message is IMsg msg)
			{
				Messages.Add(msg);
			}

			return Ok<bool>();
		}

		#region Explicit implementations

		IOk IOk.Ok()
			=> Ok();

		IOk<TValue> IOk<TValue>.Ok()
			=> Ok();

		IOk<TNext> IOk.Ok<TNext>()
			=> Ok<TNext>();

		IOkV<TNext> IOk.OkV<TNext>(TNext value)
			=> OkV(value);

		IOk<bool> IOk.True(IMsg? message)
			=> True(message);

		IOk<bool, TNext> IOk.WithState<TNext>(TNext state)
			=> this switch
			{
				IOkV<bool, TNext> x => new ROkV<bool, TNext>(x.Value, state) { Messages = Messages },
				_ => new ROk<bool, TNext>(state) { Messages = Messages, Logger = Logger }
			};

		IOk<TValue, TNext> IOk<TValue>.WithState<TNext>(TNext state)
			=> this switch
			{
				IOkV<TValue, TNext> x => new ROkV<TValue, TNext>(x.Value, state) { Messages = Messages },
				_ => new ROk<TValue, TNext>(state) { Messages = Messages, Logger = Logger }
			};

		#endregion
	}
}
