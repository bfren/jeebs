// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T>
	{
		/// <summary>
		/// Return as <see cref="Option{T}"/> wrapped in <see cref="Task{TResult}"/>
		/// </summary>
		public Task<Option<T>> AsTask =>
			Task.FromResult(this);

		internal Option() { }

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
		///    Value (if this is <see cref="Some{T}"/> and Value is not null)
		///    Reason (if this is <see cref="None{T}"/> and it has a reason)
		/// </summary>
		public override string ToString() =>
			F.OptionF.Switch(
				this,
				some: v =>
					v?.ToString() switch
					{
						string value =>
							value,

						_ =>
							"Some: " + typeof(T).ToString()
					},

				none: r =>
					r?.ToString() switch
					{
						string reason =>
							reason,

						_ =>
							"None: " + typeof(T).ToString()
					}
			);

		#region Operators

		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value) =>
			Return(value);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator ==(Option<T> l, T r) =>
			F.OptionF.Switch(
				l,
				some: v => Equals(v, r),
				none: _ => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Option</param>
		/// <param name="r">Value</param>
		public static bool operator !=(Option<T> l, T r) =>
			F.OptionF.Switch(
				l,
				some: v => !Equals(v, r),
				none: _ => true
			);

		#endregion

		#region Equals

		/// <summary>
		/// Compare this <see cref="Option{T}"/> with another object
		/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
		/// <para>If both are a <see cref="None{T}"/> this will return true</para>
		/// <para>Otherwise this will return false</para>
		/// </summary>
		/// <param name="other">Object to compare to this <see cref="Option{T}"/></param>
		public override bool Equals(object? other) =>
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
		public override int GetHashCode() =>
			this switch
			{
				Some<T> x when x.Value is T y =>
					typeof(Some<>).GetHashCode() ^ y.GetHashCode(),

				None<T> x when x.Reason is IMsg y =>
					typeof(None<>).GetHashCode() ^ y.GetHashCode(),

				None<T> _ =>
					typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode(),

				_ =>
					throw new UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion

		#region Audit

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}})"/>
		public Option<T> Audit(Action<Option<T>> audit) =>
			F.OptionF.Audit(this, audit);

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task})"/>
		public Task<Option<T>> AuditAsync(Func<Option<T>, Task> audit) =>
			F.OptionF.AuditAsync(this, audit);

		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<T> some) =>
			F.OptionF.AuditSwitch(this, some, null);

		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<IMsg?> none) =>
			F.OptionF.AuditSwitch(this, null, none);

		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.AuditSwitch(this, some, none);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some) =>
			F.OptionF.AuditSwitchAsync(this, some: v => { some?.Invoke(v); return Task.CompletedTask; }, none: null);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some) =>
			F.OptionF.AuditSwitchAsync(this, some: some, none: null);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<IMsg?> none) =>
			F.OptionF.AuditSwitchAsync(this, some: null, none: r => { none?.Invoke(r); return Task.CompletedTask; });

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<IMsg?, Task> none) =>
			F.OptionF.AuditSwitchAsync(this, some: null, none: none);

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.AuditSwitchAsync(this, some: v => { some?.Invoke(v); return Task.CompletedTask; }, none: r => { none?.Invoke(r); return Task.CompletedTask; });

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public Task<Option<T>> AuditSwitchAsync(Func<T, Task> some, Func<IMsg?, Task> none) =>
			F.OptionF.AuditSwitchAsync(this, some: some, none: none);

		#endregion

		#region Bind

		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}})"/>
		public Option<U> Bind<U>(Func<T, Option<U>> bind) =>
			F.OptionF.Bind(this, bind);

		/// <inheritdoc cref="BindAsync{T, U}(Option{T}, Func{T, Task{Option{U}}})"/>
		public Task<Option<U>> BindAsync<U>(Func<T, Task<Option<U>>> bind) =>
			F.OptionF.BindAsync(this, bind);

		#endregion

		#region Filter

		/// <inheritdoc cref="Filter{T}(Option{T}, Func{T, bool})"/>
		public Option<T> Filter(Func<T, bool> predicate) =>
			F.OptionF.Filter(this, predicate);

		/// <inheritdoc cref="FilterAsync{T}(Option{T}, Func{T, Task{bool}})"/>
		public Task<Option<T>> FilterAsync(Func<T, Task<bool>> predicate) =>
			F.OptionF.FilterAsync(this, predicate);

		#endregion

		#region Map

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler)"/>
		public Option<U> Map<U>(Func<T, U> map, Handler handler) =>
			F.OptionF.Map(this, map, handler);

		/// <inheritdoc cref="MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler)"/>
		public Task<Option<U>> MapAsync<U>(Func<T, Task<U>> map, Handler handler) =>
			F.OptionF.MapAsync(this, map, handler);

		#endregion

		#region Match

		/// <inheritdoc cref="Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, U none) =>
			F.OptionF.Match(this, some: some, none: _ => none);

		/// <inheritdoc cref="Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, Func<U> none) =>
			F.OptionF.Match(this, some: some, none: _ => none());

		/// <inheritdoc cref="Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			F.OptionF.Match(this, some: some, none: none);

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, U none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => Task.FromResult(none));

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Task<U> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none);

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Task<U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => none);

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => Task.FromResult(none()));

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none());

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => none());

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: none);

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: r => Task.FromResult(none(r)));

		/// <inheritdoc cref="MatchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg?, Task{U}})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: some, none: none);

		#endregion

		#region Switch

		/// <inheritdoc cref="Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action none) =>
			F.OptionF.Switch(this, some: some, none: _ => none());

		/// <inheritdoc cref="Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.Switch(this, some: some, none: none);

		#endregion

		#region Unwrap

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(T ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone);

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(Func<T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone());

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(Func<IMsg?, T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: ifNone);

		/// <inheritdoc cref="UnwrapSingle{T, U}(Option{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
		public Option<U> UnwrapSingle<U>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
			F.OptionF.UnwrapSingle<T, U>(this, noItems, tooMany, notAList);

		#endregion
	}
}
