// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Functions.EnumF
{
	/// <summary>
	/// See <see cref="F.EnumF.Parse(Type, string)"/>
	/// </summary>
	public sealed class NotAValidEnumMsg : WithValueMsg<Type>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="t">The type</param>
		public NotAValidEnumMsg(Type t) : base(t) { }
	}
}
