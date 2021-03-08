// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;

namespace Jm
{
	/// <summary>
	/// Abstraction to hold message format
	/// </summary>
	public abstract class WithValueMsg
	{
		public const string Format = "{0}: '{1}'.";

		internal WithValueMsg() { }
	}

	/// <summary>
	/// Message with value - the value will be output as string when <see cref="ToString"/> is called
	/// </summary>
	/// <typeparam name="TValue">Value type</typeparam>
	public abstract class WithValueMsg<TValue> : WithValueMsg, IMsg
	{
		/// <summary>
		/// Value
		/// </summary>
		public TValue Value { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Value</param>
		protected WithValueMsg(TValue value) =>
			Value = value;

		/// <summary>
		/// Return <see cref="Value"/>.ToString() or (if null) GetType().ToString();
		/// </summary>
		public override string ToString() =>
			string.Format(Format, GetType().Name, Value);
	}
}
