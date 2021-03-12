// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="DoAuditSwitch(Action{T}?, Action{IMsg?}?)"/>
		internal Task<Option<T>> DoAuditSwitchAsync(Func<T, Task>? some, Func<IMsg?, Task>? none)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return Task.FromResult(this);
			}

			// Work out which audit function to use
			Func<Task> audit = Switch<Func<Task>>(
				some: v => () => some?.Invoke(v) ?? Task.CompletedTask,
				none: r => () => none?.Invoke(r) ?? Task.CompletedTask
			);

			// Perform the audit
			try
			{
				audit();
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return Task.FromResult(this);
		}

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some) =>
			DoAuditSwitchAsync(
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: null
			);

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some) =>
			DoAuditSwitchAsync(
				some: some,
				none: null
			);

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<IMsg?> none) =>
			DoAuditSwitchAsync(
				some: null,
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<IMsg?, Task> none) =>
			DoAuditSwitchAsync(
				some: null,
				none: none
			);

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some, Action<IMsg?> none) =>
			DoAuditSwitchAsync(
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="DoAuditSwitchAsync(Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some, Func<IMsg?, Task> none) =>
			DoAuditSwitchAsync(
				some: some,
				none: none
			);
	}
}
