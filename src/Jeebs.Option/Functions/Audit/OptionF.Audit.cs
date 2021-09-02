// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// Audit the option and return unmodified<br/>
	/// Errors will not be returned as they affect the state of the object, but will be written to the console,
	/// or <see cref="LogAuditExceptions"/> if set
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="option">Option being audited</param>
	/// <param name="any">[Optional] Action will run for any <paramref name="option"/></param>
	/// <param name="some">[Optional] Action will run if <paramref name="option"/> is <see cref="Some{T}"/></param>
	/// <param name="none">[Optional] Action will run if <paramref name="option"/> is <see cref="Jeebs.Internals.None{T}"/></param>
	public static Option<T> Audit<T>(Option<T> option, Action<Option<T>>? any, Action<T>? some, Action<IMsg>? none)
	{
		// Do nothing if the user gave us nothing to do!
		if (any == null && some == null && none == null)
		{
			return option;
		}

		// Work out which audit function to use
		Action audit = Switch<T, Action>(
			option,
			some: v => () => some?.Invoke(v),
			none: r => () => none?.Invoke(r)
		);

		// Perform the audit
		try
		{
			any?.Invoke(option);
			audit();
		}
		catch (Exception e)
		{
			HandleAuditException(e);
		}

		// Return the original object
		return option;
	}
}
