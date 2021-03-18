// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: AuditSwitchAsync
	/// </summary>
	public static class OptionExtensions_AuditSwitchAsync
	{
		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Action<T> some) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: null
			);

		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Func<T, Task> some) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: some,
				none: null
			);

		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Action<IMsg> none) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: null,
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Func<IMsg, Task> none) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: null,
				none: none
			);

		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Action<T> some, Action<IMsg> none) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: v => { some?.Invoke(v); return Task.CompletedTask; },
				none: r => { none?.Invoke(r); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="F.OptionF.AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Func<T, Task> some, Func<IMsg, Task> none) =>
			F.OptionF.AuditSwitchAsync(
				@this,
				some: some,
				none: none
			);
	}
}
