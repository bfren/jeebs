// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Functions.EnumF
{
	/// <summary>
	/// See <see cref="JeebsF.EnumF.Parse(Type, string)"/>
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
