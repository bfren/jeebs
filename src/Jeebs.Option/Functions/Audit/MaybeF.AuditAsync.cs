// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static async Task<Maybe<T>> AuditAsync<T>(Maybe<T> maybe, Func<Maybe<T>, Task>? any, Func<T, Task>? some, Func<Msg, Task>? none)
	{
		// Do nothing if the user gave us nothing to do!
		if (any is null && some is null && none is null)
		{
			return maybe;
		}

		// Work out which audit function to use
		var audit = Switch<T, Func<Task>>(
			maybe,
			some: v => () => some?.Invoke(v) ?? Task.CompletedTask,
			none: r => () => none?.Invoke(r) ?? Task.CompletedTask
		);

		// Perform the audit
		try
		{
			if (any != null)
			{
				await any(maybe).ConfigureAwait(false);
			}

			await audit().ConfigureAwait(false);
		}
		catch (Exception e)
		{
			HandleAuditException(e);
		}

		// Return the original object
		return maybe;
	}

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static async Task<Maybe<T>> AuditAsync<T>(Task<Maybe<T>> maybe, Action<Maybe<T>>? any, Action<T>? some, Action<Msg>? none) =>
		await AuditAsync(await maybe.ConfigureAwait(false),
			x => { any?.Invoke(x); return Task.CompletedTask; },
			x => { some?.Invoke(x); return Task.CompletedTask; },
			x => { none?.Invoke(x); return Task.CompletedTask; }
		).ConfigureAwait(false);

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static async Task<Maybe<T>> AuditAsync<T>(Task<Maybe<T>> maybe, Func<Maybe<T>, Task>? any, Func<T, Task>? some, Func<Msg, Task>? none) =>
		await AuditAsync(await maybe.ConfigureAwait(false), any, some, none).ConfigureAwait(false);
}
