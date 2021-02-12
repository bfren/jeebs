using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Functions.EnumF
{
	/// <summary>
	/// See <see cref="F.EnumF.Parse(Type, string)"/>
	/// </summary>
	/// <typeparam name="TFrom">From Enum</typeparam>
	/// <typeparam name="TTo">To Enum</typeparam>
	public sealed class ValueNotInReceivingEnumMsg<TFrom, TTo> : WithValueMsg<TFrom>
		where TFrom : struct, Enum
		where TTo : struct, Enum
	{
		/// <summary>
		///  Set value
		/// </summary>
		/// <param name="value">Value</param>
		public ValueNotInReceivingEnumMsg(TFrom value) : base(value) { }

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString() =>
			$"'{Value}' is not a valid {typeof(TTo)}.";
	}
}
