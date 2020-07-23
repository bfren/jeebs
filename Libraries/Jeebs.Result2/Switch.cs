using System;
namespace Jeebs
{
	public sealed class Switch<TValue>
	{
		public Func<IOk<TValue>, IOk<TValue>>? IfOk { get; set; }
		public Func<IOkV<TValue>, IOkV<TValue>>? IfOkV { get; set; }
		public Func<IError<TValue>, IError<TValue>>? IfError { get; set; }

		public bool Null { get => IfOk == null && IfOkV == null && IfError == null; }
	}

	public sealed class Switch<TValue, TState>
	{
		public Func<IOk<TValue, TState>, IOk<TValue, TState>>? IfOk { get; set; }
		public Func<IOkV<TValue, TState>, IOkV<TValue, TState>>? IfOkV { get; set; }
		public Func<IError<TValue, TState>, IError<TValue, TState>>? IfError { get; set; }

		public bool Null { get => IfOk == null && IfOkV == null && IfError == null; }
	}
}
