// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jm.Functions.DateTimeF
{
	/// <summary>
	/// See <see cref="JeebsF.DateTimeF.FromFormat(string, string)"/>
	/// </summary>
	public sealed class InvalidDateTimeMsg : WithValueMsg<string>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="dt">Date/Time string</param>
		public InvalidDateTimeMsg(string dt) : base(dt) { }
	}
}
