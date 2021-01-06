using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Message with value - the value will be output as string when <see cref="ToString"/> is called
	/// </summary>
	/// <typeparam name="TValue">Value type</typeparam>
	public abstract class WithValueMsg<TValue> : IMsg
	{
		/// <summary>
		/// Value
		/// </summary>
		public TValue Value { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Value</param>
		protected WithValueMsg(TValue value)
			=> Value = value;

		/// <summary>
		/// Return <see cref="Value"/>.ToString() or (if null) GetType().ToString();
		/// </summary>
		public override string ToString()
			=> Value?.ToString() ?? GetType().ToString();
	}
}
