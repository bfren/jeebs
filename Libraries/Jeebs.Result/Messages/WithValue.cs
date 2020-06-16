using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Message with value - the value will be output as string when <see cref="ToString"/> is called
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public class WithValue<T> : IMessage
	{
		/// <summary>
		/// Value
		/// </summary>
		public T Val { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Value</param>
		public WithValue(T value) => Val = value;

		/// <summary>
		/// Return <see cref="Val"/>.ToString() or (if null) base.ToString();
		/// </summary>
		public override string ToString() => Val?.ToString() ?? base.ToString();
	}

	/// <summary>
	/// Special case: String value
	/// </summary>
	public class WithString : WithValue<string>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">String</param>
		public WithString(string value) : base(value) { }
	}

	/// <summary>
	/// Special case: String value
	/// </summary>
	public class WithInt32 : WithValue<int>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Integer</param>
		public WithInt32(int value) : base(value) { }
	}
}
