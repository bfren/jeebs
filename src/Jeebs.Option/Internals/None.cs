﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Internals;

/// <summary>
/// 'None' option - used to replace null returns (see <seealso cref="Some{T}"/>)
/// </summary>
/// <typeparam name="T">Option value type</typeparam>
public sealed record class None<T> : Option<T>
{
	/// <summary>
	/// Sometimes a reason for the 'None' value may be set
	/// </summary>
	public IMsg Reason { get; private init; }

	/// <summary>
	/// Only allow internal creation - via <see cref="F.OptionF.None{T}(IMsg)"/> etc.
	/// </summary>
	/// <param name="reason">Reason message for this <see cref="None{T}"/></param>
	internal None(IMsg reason) =>
		Reason = reason;

	/// <summary>
	/// Return:
	///    Reason (if one is set)
	/// </summary>
	public override string ToString() =>
		base.ToString();
}
