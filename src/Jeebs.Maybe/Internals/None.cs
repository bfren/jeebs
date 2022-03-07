﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Internals;

/// <summary>
/// 'None' Maybe - used to replace null returns (see <seealso cref="Some{T}"/>)
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public sealed record class None<T> : Maybe<T>
{
	/// <summary>
	/// A reason for the 'None' value must always be set
	/// </summary>
	public Msg Reason { get; private init; }

	/// <summary>
	/// Only allow internal creation by None() functions
	/// </summary>
	/// <param name="reason">Reason message for this <see cref="None{T}"/></param>
	internal None(Msg reason) =>
		Reason = reason;
}