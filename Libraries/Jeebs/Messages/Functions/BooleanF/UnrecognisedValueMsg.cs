// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jm.Functions.BooleanF
{
	/// <summary>
	/// See <see cref="JeebsF.BooleanF.Parse{T}(T)"/>
	/// </summary>
	public sealed class UnrecognisedValueMsg : WithValueMsg<string>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="value">Unrecognised boolean value</param>
		public UnrecognisedValueMsg(string value) : base(value) { }
	}
}
