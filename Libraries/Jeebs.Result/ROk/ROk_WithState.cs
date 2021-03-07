// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <inheritdoc cref="IOk{TValue, TState}"/>
	public class ROk<TValue, TState> : R<TValue, TState>, IOk<TValue, TState>
	{
		internal ROk(TState state) : base(state) { }

		/// <inheritdoc cref="IOk{TValue, TState}.Ok"/>
		public IOk<TValue, TState> Ok() =>
			Ok<TValue>();

		/// <inheritdoc cref="IOk{TValue, TState}.Ok{TNext}"/>
		public IOk<TNext, TState> Ok<TNext>() =>
			this switch
			{
				IOk<TNext, TState> ok =>
					ok,

				_ =>
					new ROk<TNext, TState>(State) { Messages = Messages, Logger = Logger }
			};

		/// <inheritdoc cref="IOk{TValue, TState}.OkV{TNext}(TNext)"/>
		public IOkV<TNext, TState> OkV<TNext>(TNext value) =>
			new ROkV<TNext, TState>(value, State) { Messages = Messages, Logger = Logger };

		/// <inheritdoc/>
		public IOkV<bool, TState> OkTrue(IMsg? message = null) =>
			OkBoolean(true, message);

		/// <inheritdoc/>
		public IOkV<bool, TState> OkFalse(IMsg? message = null) =>
			OkBoolean(false, message);

		private IOkV<bool, TState> OkBoolean(bool value, IMsg? message = null)
		{
			if (message is IMsg msg)
			{
				Messages.Add(msg);
			}

			return OkV(value);
		}

		#region Explicit implementations

		IOk IOk.Ok() =>
			Ok();

		IOk<TValue> IOk<TValue>.Ok() =>
			Ok();

		IOk<TNext> IOk.Ok<TNext>() =>
			Ok<TNext>();

		IOkV<TNext> IOk.OkV<TNext>(TNext value) =>
			OkV(value);

		IOkV<bool> IOk.OkTrue(IMsg? message) =>
			OkTrue(message);

		IOkV<bool> IOk.OkFalse(IMsg? message) =>
			OkFalse(message);

		IOk<bool, TNext> IOk.WithState<TNext>(TNext state) =>
			this switch
			{
				IOkV<bool, TNext> x =>
					new ROkV<bool, TNext>(x.Value, state) { Messages = Messages },

				_ =>
					new ROk<bool, TNext>(state) { Messages = Messages, Logger = Logger }
			};

		IOk<TValue, TNext> IOk<TValue>.WithState<TNext>(TNext state) =>
			this switch
			{
				IOkV<TValue, TNext> x =>
					new ROkV<TValue, TNext>(x.Value, state) { Messages = Messages },

				_ =>
					new ROk<TValue, TNext>(state) { Messages = Messages, Logger = Logger }
			};

		#endregion
	}
}
