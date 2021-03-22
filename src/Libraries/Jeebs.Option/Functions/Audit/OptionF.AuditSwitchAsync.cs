// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditSwitchAsync<T>(Option<T> option, Func<T, Task>? some, Func<IMsg, Task>? none)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return option;
			}

			// Work out which audit function to use
			Func<Task> audit = Switch<T, Func<Task>>(
				option,
				some: v => () => some?.Invoke(v) ?? Task.CompletedTask,
				none: r => () => none?.Invoke(r) ?? Task.CompletedTask
			);

			// Perform the audit
			try
			{
				await audit();
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return option;
		}

		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditSwitchAsync<T>(Task<Option<T>> option, Action<T>? some, Action<IMsg>? none) =>
			await AuditSwitchAsync(await option,
				x => { some?.Invoke(x); return Task.CompletedTask; },
				x => { none?.Invoke(x); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditSwitchAsync<T>(Task<Option<T>> option, Func<T, Task>? some, Func<IMsg, Task>? none) =>
			await AuditSwitchAsync(await option, some, none);
	}
}
