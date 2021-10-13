// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Option{T}"/> Extensions: AuditAsync
/// </summary>
public static class OptionExtensions_AuditAsync
{
	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<Option<T>> any) =>
		F.OptionF.AuditAsync(
			@this,
			any: x => { any(x); return Task.CompletedTask; },
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<Option<T>, Task> any) =>
		F.OptionF.AuditAsync(
			@this,
			any: any,
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<T> some) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: null
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<T, Task> some) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: null
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<IMsg> none) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<IMsg, Task> none) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: none
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<T> some, Action<IMsg> none) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.OptionF.Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<T, Task> some, Func<IMsg, Task> none) =>
		F.OptionF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: none
		);
}
