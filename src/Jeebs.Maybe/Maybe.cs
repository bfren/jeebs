// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using Jeebs.Internals;
using static F.MaybeF;

namespace Jeebs;

/// <summary>
/// Maybe type - enables null-safe returning by wrapping value in <see cref="Internals.Some{T}"/> and null in <see cref="Internals.None{T}"/>
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public abstract record class Maybe<T> : IEquatable<Maybe<T>>
{
	/// <summary>
	/// Return as <see cref="Maybe{T}"/> wrapped in <see cref="Task{TResult}"/>
	/// </summary>
	[JsonIgnore]
	public Task<Maybe<T>> AsTask =>
		Task.FromResult(this);

	/// <summary>
	/// Return as <see cref="Maybe{T}"/> wrapped in <see cref="ValueTask{TResult}"/>
	/// </summary>
	[JsonIgnore]
	public ValueTask<Maybe<T>> AsValueTask =>
		ValueTask.FromResult(this);

	/// <summary>
	/// Whether or not this is <see cref="Internals.Some{T}"/>
	/// </summary>
	[JsonIgnore]
	public bool IsSome =>
		this is Some<T>;

	/// <summary>
	/// Whether or not this is <see cref="Internals.None{T}"/>
	/// </summary>
	[JsonIgnore]
	public bool IsNone =>
		this is None<T>;

	/// <summary>
	/// Returns an enumerator to enable use in foreach blocks
	/// </summary>
	public IEnumerator<T> GetEnumerator()
	{
		if (this is Some<T> some)
		{
			yield return some.Value;
		}
	}

	/// <summary>
	/// Return:
	///    Value (if this is <see cref="Internals.Some{T}"/> and Value is not null)
	///    Reason (if this is <see cref="Internals.None{T}"/> and it has a reason)
	/// </summary>
	public sealed override string ToString() =>
		F.MaybeF.Switch(
			this,
			some: v =>
				v?.ToString() switch
				{
					string value =>
						value,

					_ =>
						$"Some: {typeof(T)}"
				},

			none: r =>
				r.ToString() switch
				{
					string when r is ExceptionMsg e =>
						$"{e.GetType()}: {e.Value.Message}",

					string reason =>
						reason,

					_ =>
						$"None: {typeof(T)}"
				}
		);

	#region Operators

	/// <summary>
	/// Wrap a value in a <see cref="Internals.Some{T}"/>
	/// </summary>
	/// <param name="value">Value</param>
	public static implicit operator Maybe<T>(T value) =>
		value switch
		{
			T =>
				new Some<T>(value), // Some<T> is only created by Some() functions and implicit operator

			_ =>
				None<T, M.NullValueMsg>()
		};

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Internals.Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Maybe</param>
	/// <param name="r">Value</param>
	public static bool operator ==(Maybe<T> l, T r) =>
		F.MaybeF.Switch(
			l,
			some: v => Equals(v, r),
			none: _ => false
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Internals.Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Maybe</param>
	/// <param name="r">Value</param>
	public static bool operator !=(Maybe<T> l, T r) =>
		F.MaybeF.Switch(
			l,
			some: v => !Equals(v, r),
			none: _ => true
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Internals.Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value</param>
	/// <param name="r">Maybe</param>
	public static bool operator ==(T l, Maybe<T> r) =>
		F.MaybeF.Switch(
			r,
			some: v => Equals(v, l),
			none: _ => false
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Internals.Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value</param>
	/// <param name="r">Maybe</param>
	public static bool operator !=(T l, Maybe<T> r) =>
		F.MaybeF.Switch(
			r,
			some: v => !Equals(v, l),
			none: _ => true
		);

	#endregion Operators

	#region Equals

	/// <inheritdoc cref="Equals(object?)"/>
	/// <param name="other">Comparison object</param>
	public virtual bool Equals(Maybe<T>? other) =>
		this switch
		{
			Some<T> x when other is Some<T> y =>
				Equals(x.Value, y.Value),

			None<T> x when other is None<T> y =>
				Equals(x.Reason, y.Reason),

			_ =>
				false
		};

	/// <summary>
	/// Generate custom HashCode
	/// </summary>
	/// <exception cref="UnknownMaybeException"></exception>
	public override int GetHashCode() =>
		this switch
		{
			Some<T> x when x.Value is T y =>
				typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode() ^ y.GetHashCode(),

			Some<T> _ =>
				typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode(),

			None<T> x =>
				typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode() ^ x.Reason.GetHashCode(),

			_ =>
				throw new UnknownMaybeException() // as Maybe<T> is internal implementation only this should never happen...
		};

	#endregion Equals

	#region Audit

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public Maybe<T> Audit(Action<Maybe<T>> any) =>
		F.MaybeF.Audit(this, any, null, null);

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public Maybe<T> Audit(Action<T> some) =>
		F.MaybeF.Audit(this, null, some, null);

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public Maybe<T> Audit(Action<Msg> none) =>
		F.MaybeF.Audit(this, null, null, none);

	/// <inheritdoc cref="Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{Msg}?)"/>
	public Maybe<T> Audit(Action<T> some, Action<Msg> none) =>
		F.MaybeF.Audit(this, null, some, none);

	/// <inheritdoc cref="AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{Msg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<Maybe<T>, Task> any) =>
		F.MaybeF.AuditAsync(this, any, null, null);

	/// <inheritdoc cref="AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{Msg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some) =>
		F.MaybeF.AuditAsync(this, null, some, null);

	/// <inheritdoc cref="AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{Msg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<Msg, Task> none) =>
		F.MaybeF.AuditAsync(this, null, null, none);

	/// <inheritdoc cref="AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{Msg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some, Func<Msg, Task> none) =>
		F.MaybeF.AuditAsync(this, null, some, none);

	#endregion Audit

	#region Bind

	/// <inheritdoc cref="Bind{T, U}(Maybe{T}, Func{T, Maybe{U}})"/>
	public Maybe<U> Bind<U>(Func<T, Maybe<U>> bind) =>
		F.MaybeF.Bind(this, bind);

	/// <inheritdoc cref="BindAsync{T, U}(Maybe{T}, Func{T, Task{Maybe{U}}})"/>
	public Task<Maybe<U>> BindAsync<U>(Func<T, Task<Maybe<U>>> bind) =>
		F.MaybeF.BindAsync(this, bind);

	#endregion Bind

	#region Filter

	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public Maybe<T> Filter(Func<T, bool> predicate) =>
		F.MaybeF.Filter(this, predicate);

	/// <inheritdoc cref="FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, bool> predicate) =>
		F.MaybeF.FilterAsync(this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, Task<bool>> predicate) =>
		F.MaybeF.FilterAsync(this, predicate);

	#endregion Filter

	#region IfNull

	/// <inheritdoc cref="F.MaybeF.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Maybe<T> IfNull(Func<Maybe<T>> ifNull) =>
		F.MaybeF.IfNull(this, ifNull);

	/// <inheritdoc cref="IfNull{T, TMsg}(Maybe{T}, Func{TMsg})"/>
	public Maybe<T> IfNull<TMsg>(Func<TMsg> ifNull)
		where TMsg : Msg =>
		F.MaybeF.IfNull(this, ifNull);

	/// <inheritdoc cref="F.MaybeF.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Task<Maybe<T>> IfNullAsync(Func<Task<Maybe<T>>> ifNull) =>
		F.MaybeF.IfNullAsync(this, ifNull);

	#endregion IfNull

	#region IfSome

	/// <inheritdoc cref="F.MaybeF.IfSome{T}(Maybe{T}, Action{T})"/>
	public Maybe<T> IfSome(Action<T> ifSome) =>
		F.MaybeF.IfSome(this, ifSome);

	/// <inheritdoc cref="F.MaybeF.IfSome{T}(Maybe{T}, Action{T})"/>
	public Task<Maybe<T>> IfSomeAsync(Func<T, Task> ifSome) =>
		F.MaybeF.IfSomeAsync(this, ifSome);

	#endregion IfSome

	#region Map

	/// <inheritdoc cref="Map{T, U}(Maybe{T}, Func{T, U}, Handler)"/>
	public Maybe<U> Map<U>(Func<T, U> map, Handler handler) =>
		F.MaybeF.Map(this, map, handler);

	/// <inheritdoc cref="MapAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Handler)"/>
	public Task<Maybe<U>> MapAsync<U>(Func<T, Task<U>> map, Handler handler) =>
		F.MaybeF.MapAsync(this, map, handler);

	#endregion Map

	#region Switch

	/// <inheritdoc cref="F.MaybeF.Switch{T}(Maybe{T}, Action{T}, Action{Msg})"/>
	public void Switch(Action<T> some, Action none) =>
		F.MaybeF.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="F.MaybeF.Switch{T}(Maybe{T}, Action{T}, Action{Msg})"/>
	public void Switch(Action<T> some, Action<Msg> none) =>
		F.MaybeF.Switch(this, some: some, none: none);

	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public U Switch<U>(Func<T, U> some, U none) =>
		F.MaybeF.Switch(this, some: some, none: _ => none);

	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public U Switch<U>(Func<T, U> some, Func<U> none) =>
		F.MaybeF.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="Switch{T, U}(Maybe{T}, Func{T, U}, Func{Msg, U})"/>
	public U Switch<U>(Func<T, U> some, Func<Msg, U> none) =>
		F.MaybeF.Switch(this, some: some, none: none);

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, U none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, U> some, Task<U> none) =>
		F.MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Task<U> none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: _ => none);

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
		F.MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: _ => none());

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, U> some, Func<Msg, Task<U>> none) =>
		F.MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<Msg, U> none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{Msg, Task{U}})"/>
	public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<Msg, Task<U>> none) =>
		F.MaybeF.SwitchAsync(this, some: some, none: none);

	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, Maybe<T>>? ifTrue = null, Func<T, Maybe<T>>? ifFalse = null) =>
		F.MaybeF.SwitchIf(this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Msg})"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, Msg> ifFalse) =>
		F.MaybeF.SwitchIf(this, check, ifFalse);

	#endregion Switch

	#region Unwrap

	/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
	public T Unwrap(T ifNone) =>
		F.MaybeF.Unwrap(this, ifNone: _ => ifNone);

	/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
	public T Unwrap(Func<T> ifNone) =>
		F.MaybeF.Unwrap(this, ifNone: _ => ifNone());

	/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{Msg, T})"/>
	public T Unwrap(Func<Msg, T> ifNone) =>
		F.MaybeF.Unwrap(this, ifNone: ifNone);

	/// <inheritdoc cref="UnwrapSingle{T, U}(Maybe{T}, Func{Msg}?, Func{Msg}?, Func{Msg}?)"/>
	public Maybe<U> UnwrapSingle<U>(Func<Msg>? noItems = null, Func<Msg>? tooMany = null, Func<Msg>? notAList = null) =>
		UnwrapSingle<T, U>(this, noItems, tooMany, notAList);

	#endregion Unwrap
}
