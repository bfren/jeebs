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
	public class WithValueMsg<TValue> : IMsg
	{
		/// <summary>
		/// Value
		/// </summary>
		public TValue Value { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Value</param>
		public WithValueMsg(TValue value)
			=> Value = value;

		/// <summary>
		/// Return <see cref="Val"/>.ToString() or (if null) base.ToString();
		/// </summary>
		public override string ToString()
			=> Value?.ToString() ?? base.ToString();
	}
}
