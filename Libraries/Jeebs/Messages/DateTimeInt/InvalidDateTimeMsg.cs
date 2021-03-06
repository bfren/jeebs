// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jm.DateTimeInt
{
	/// <summary>
	/// See <see cref="Jeebs.DateTimeInt"/>
	/// </summary>
	public sealed class InvalidDateTimeMsg : WithValueMsg<(string part, Jeebs.DateTimeInt dt)>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="part">The part with an error</param>
		/// <param name="dt">Jeebs.DateTimeInt</param>
		public InvalidDateTimeMsg(string part, Jeebs.DateTimeInt dt) : base((part, dt)) { }

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString() =>
			$"Invalid {Value.part} - 'Y:{Value.dt.Year} M:{Value.dt.Minute} D:{Value.dt.Day} H:{Value.dt.Hour} m:{Value.dt.Minute}'.";
	}
}
