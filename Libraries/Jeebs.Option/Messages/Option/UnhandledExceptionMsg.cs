// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace Jm.Option
{
	/// <summary>
	/// See <see cref="Jeebs.OptionF.Catch{T}(Func{Option{T}}, Jeebs.OptionF.Handler?)"/>
	/// </summary>
	public sealed record UnhandledExceptionMsg(Exception Exception) : IMsg
	{
		/// <summary>
		/// Output the Exception string
		/// </summary>
		public override string ToString() =>
			string.Format("{0}: {1}", nameof(UnhandledExceptionMsg), Exception);
	}
}
