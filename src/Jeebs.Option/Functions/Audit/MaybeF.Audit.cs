// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Audit the Maybe and return unmodified<br/>
	/// Errors will not be returned as they affect the state of the object, but will be written to the console,
	/// or <see cref="LogAuditExceptions"/> if set
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being audited</param>
	/// <param name="any">[Optional] Action will run for any <paramref name="maybe"/></param>
	/// <param name="some">[Optional] Action will run if <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/></param>
	/// <param name="none">[Optional] Action will run if <paramref name="maybe"/> is <see cref="Jeebs.Internals.None{T}"/></param>
	public static Maybe<T> Audit<T>(Maybe<T> maybe, Action<Maybe<T>>? any, Action<T>? some, Action<Msg>? none)
	{
		// Do nothing if the user gave us nothing to do!
		if (any is null && some is null && none is null)
		{
			return maybe;
		}

		// Work out which audit function to use
		var audit = Switch<T, Action>(
			maybe,
			some: v => () => some?.Invoke(v),
			none: r => () => none?.Invoke(r)
		);

		// Perform the audit
		try
		{
			any?.Invoke(maybe);
			audit();
		}
		catch (Exception e)
		{
			HandleAuditException(e);
		}

		// Return the original object
		return maybe;
	}
}
