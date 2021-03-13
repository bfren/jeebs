// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		internal Task<Option<T>> DoAuditSwitchAsync(Func<T, Task>? some, Func<IMsg?, Task>? none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some,
				none
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: null
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: some,
				none: null
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<IMsg?> none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: null,
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<IMsg?, Task> none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: null,
				none: none
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some, Func<IMsg?, Task> none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some: some,
				none: none
			);
	}
}
