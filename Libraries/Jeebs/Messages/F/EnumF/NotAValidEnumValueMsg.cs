using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.EnumF
{
	/// <summary>
	/// See <see cref="F.EnumF.Parse(Type, string)"/>
	/// </summary>
	public sealed class NotAValidEnumValueMsg : WithValueMsg<string>
	{
		private readonly Type type;

		/// <summary>
		///  Set value
		/// </summary>
		/// <param name="type">Enum type</param>
		/// <param name="value">Value</param>
		public NotAValidEnumValueMsg(Type type, string value) : base(value)
			=> this.type = type;

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString()
			=> $"'{Value}' is not a valid value of {type.GetType()}.";
	}

	/// <summary>
	/// See <see cref="F.EnumF.Parse{T}(string)"/>
	/// </summary>
	/// <typeparam name="TEnum">Enum type</typeparam>
	public sealed class NotAValidEnumValueMsg<TEnum> : WithValueMsg<string>
		where TEnum : System.Enum
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
