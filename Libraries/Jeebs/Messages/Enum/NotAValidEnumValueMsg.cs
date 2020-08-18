using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Enum
{
	/// <summary>
	/// See <see cref="Jeebs.Enumerated.Parse{T}(string, T[])"/>
	/// </summary>
	/// <typeparam name="TEnum">Enum type</typeparam>
	public sealed class NotAValidEnumValueMsg<TEnum> : WithValueMsg<string>
		where TEnum : Jeebs.Enumerated
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
			=> $"'{Value}' is not a valid value of {typeof(TEnum)}.";
	}
}
