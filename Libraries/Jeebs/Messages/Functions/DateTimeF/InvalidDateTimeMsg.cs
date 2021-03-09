// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jm.Functions.DateTimeF
{
	/// <summary>
	/// See <see cref="F.DateTimeF.FromFormat(string, string)"/>
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
