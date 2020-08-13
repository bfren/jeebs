using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Enum
{
	/// <summary>
	/// See <see cref="Jeebs.Enum.Parse{T}(string, T[])"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class NotAValidEnumValueMsg<T> : WithValueMsg<string>
		where T : Jeebs.Enum
	{
		/// <summary>
		///  Set value
		/// </summary>
		/// <param name="value">Value</param>
		public NotAValidEnumValueMsg(string value) : base(value) { }

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString()
			=> $"'{Value}' is not a valid {typeof(T)}.";
	}
}
