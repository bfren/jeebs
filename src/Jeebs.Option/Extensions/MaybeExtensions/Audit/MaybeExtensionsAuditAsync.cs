// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
namespace Jeebs;

/// <summary>
/// <see cref="Maybe{T}"/> Extensions: AuditAsync
/// </summary>
public static class OptionExtensionsAuditAsync
{
	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<Maybe<T>> any) =>
		F.MaybeF.AuditAsync(
			@this,
			any: x => { any(x); return Task.CompletedTask; },
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>, Task> any) =>
		F.MaybeF.AuditAsync(
			@this,
			any: any,
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: null
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: null
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<Msg> none) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<Msg, Task> none) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: none
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some, Action<Msg> none) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some, Func<Msg, Task> none) =>
		F.MaybeF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: none
		);
}
