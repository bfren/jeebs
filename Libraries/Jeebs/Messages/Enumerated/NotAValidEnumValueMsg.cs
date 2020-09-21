using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Enumerated
{
	/// <summary>
	/// See <see cref="Jeebs.Enumerated.Parse{T}(string, T[])"/>
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	public sealed class NotAValidEnumeratedValueMsg<T> : WithValueMsg<string>
		where T : Jeebs.Enumerated
	{
		/// <summary>
		///  Set value
		/// </summary>
		/// <param name="value">Value</param>
		public NotAValidEnumeratedValueMsg(string value) : base(value) { }

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString()
			=> $"'{Value}' is not a valid value of {typeof(T)}.";
	}
}
