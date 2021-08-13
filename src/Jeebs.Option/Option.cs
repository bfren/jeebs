// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract record class Option<T> : IEquatable<Option<T>>
	{
		/// <summary>
		/// Return as <see cref="Option{T}"/> wrapped in <see cref="Task{TResult}"/>
		/// </summary>
		[JsonIgnore]
		public Task<Option<T>> AsTask =>
			Task.FromResult(this);

		/// <summary>
		/// Whether or not this is <see cref="Some{T}"/>
		/// </summary>
		[JsonIgnore]
		public bool IsSome =>
			this is Some<T>;

		/// <summary>
		/// Whether or not this is <see cref="None{T}"/>
		/// </summary>
		[JsonIgnore]
		public bool IsNone =>
			this is None<T>;

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
							$"Some: {typeof(T)}"
					},

				none: r =>
					r.ToString() switch
					{
						string when r is IExceptionMsg e =>
							$"{e.GetType()}: {e.Exception.Message}",

						string reason =>
							reason,

						_ =>
							$"None: {typeof(T)}"
					}
			);

		#region Operators

		/// <summary>
		/// Wrap a value in a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="value">Value</param>
		public static implicit operator Option<T>(T value) =>
			value switch
			{
				T =>
					new Some<T>(value), // Some<T> is only created by Return() functions and implicit operator

				_ =>
					None<T, Msg.NullValueMsg>()
			};

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

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Value</param>
		/// <param name="r">Option</param>
		public static bool operator ==(T l, Option<T> r) =>
			F.OptionF.Switch(
				r,
				some: v => Equals(v, l),
				none: _ => false
			);

		/// <summary>
		/// Compare an option type with a value type
		/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
		/// </summary>
		/// <param name="l">Value</param>
		/// <param name="r">Option</param>
		public static bool operator !=(T l, Option<T> r) =>
			F.OptionF.Switch(
				r,
				some: v => !Equals(v, l),
				none: _ => true
			);

		#endregion

		#region Equals

		/// <inheritdoc cref="Equals(object?)"/>
		/// <param name="other">Comparison object</param>
		public virtual bool Equals(Option<T>? other) =>
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
					typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode() ^ y.GetHashCode(),

				Some<T> _ =>
					typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode(),

				None<T> x =>
					typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode() ^ x.Reason.GetHashCode(),

				_ =>
					throw new UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		#endregion

		#region Audit

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public Option<T> Audit(Action<Option<T>> any) =>
			F.OptionF.Audit(this, any, null, null);

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public Option<T> Audit(Action<T> some) =>
			F.OptionF.Audit(this, null, some, null);

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public Option<T> Audit(Action<IMsg> none) =>
			F.OptionF.Audit(this, null, null, none);

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public Option<T> Audit(Action<T> some, Action<IMsg> none) =>
			F.OptionF.Audit(this, null, some, none);

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public Task<Option<T>> AuditAsync(Func<Option<T>, Task> any) =>
			F.OptionF.AuditAsync(this, any, null, null);

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public Task<Option<T>> AuditAsync(Func<T, Task> some) =>
			F.OptionF.AuditAsync(this, null, some, null);

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public Task<Option<T>> AuditAsync(Func<IMsg, Task> none) =>
			F.OptionF.AuditAsync(this, null, null, none);

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
		public Task<Option<T>> AuditAsync(Func<T, Task> some, Func<IMsg, Task> none) =>
			F.OptionF.AuditAsync(this, null, some, none);

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
		public Task<Option<T>> FilterAsync(Func<T, bool> predicate) =>
			F.OptionF.FilterAsync(this, x => Task.FromResult(predicate(x)));

		/// <inheritdoc cref="FilterAsync{T}(Option{T}, Func{T, Task{bool}})"/>
		public Task<Option<T>> FilterAsync(Func<T, Task<bool>> predicate) =>
			F.OptionF.FilterAsync(this, predicate);

		#endregion

		#region IfNull

		/// <inheritdoc cref="F.OptionF.IfNull{T}(Option{T}, Func{Option{T}})"/>
		public Option<T> IfNull(Func<Option<T>> ifNull) =>
			F.OptionF.IfNull(this, ifNull);

		/// <inheritdoc cref="IfNull{T, TMsg}(Option{T}, Func{TMsg})"/>
		public Option<T> IfNull<TMsg>(Func<TMsg> ifNull)
			where TMsg : IMsg =>
			F.OptionF.IfNull(this, ifNull);

		/// <inheritdoc cref="F.OptionF.IfNull{T}(Option{T}, Func{Option{T}})"/>
		public Task<Option<T>> IfNullAsync(Func<Task<Option<T>>> ifNull) =>
			F.OptionF.IfNullAsync(this, ifNull);

		#endregion

		#region IfSome

		/// <inheritdoc cref="F.OptionF.IfSome{T}(Option{T}, Action{T})"/>
		public Option<T> IfSome(Action<T> ifSome) =>
			F.OptionF.IfSome(this, ifSome);

		/// <inheritdoc cref="F.OptionF.IfSome{T}(Option{T}, Action{T})"/>
		public Task<Option<T>> IfSomeAsync(Func<T, Task> ifSome) =>
			F.OptionF.IfSomeAsync(this, ifSome);

		#endregion

		#region Map

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler)"/>
		public Option<U> Map<U>(Func<T, U> map, Handler handler) =>
			F.OptionF.Map(this, map, handler);

		/// <inheritdoc cref="MapAsync{T, U}(Option{T}, Func{T, Task{U}}, Handler)"/>
		public Task<Option<U>> MapAsync<U>(Func<T, Task<U>> map, Handler handler) =>
			F.OptionF.MapAsync(this, map, handler);

		#endregion

		#region Switch

		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg})"/>
		public void Switch(Action<T> some, Action none) =>
			F.OptionF.Switch(this, some: some, none: _ => none());

		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg})"/>
		public void Switch(Action<T> some, Action<IMsg> none) =>
			F.OptionF.Switch(this, some: some, none: none);

		/// <inheritdoc cref="Switch{T, U}(Option{T}, Func{T, U}, Func{IMsg, U})"/>
		public U Switch<U>(Func<T, U> some, U none) =>
			F.OptionF.Switch(this, some: some, none: _ => none);

		/// <inheritdoc cref="Switch{T, U}(Option{T}, Func{T, U}, Func{IMsg, U})"/>
		public U Switch<U>(Func<T, U> some, Func<U> none) =>
			F.OptionF.Switch(this, some: some, none: _ => none());

		/// <inheritdoc cref="Switch{T, U}(Option{T}, Func{T, U}, Func{IMsg, U})"/>
		public U Switch<U>(Func<T, U> some, Func<IMsg, U> none) =>
			F.OptionF.Switch(this, some: some, none: none);

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, U none) =>
			F.OptionF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none));

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, U> some, Task<U> none) =>
			F.OptionF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none);

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Task<U> none) =>
			F.OptionF.SwitchAsync(this, some: some, none: _ => none);

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			F.OptionF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none()));

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			F.OptionF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none());

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			F.OptionF.SwitchAsync(this, some: some, none: _ => none());

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, U> some, Func<IMsg, Task<U>> none) =>
			F.OptionF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: none);

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<IMsg, U> none) =>
			F.OptionF.SwitchAsync(this, some: some, none: r => Task.FromResult(none(r)));

		/// <inheritdoc cref="SwitchAsync{T, U}(Option{T}, Func{T, Task{U}}, Func{IMsg, Task{U}})"/>
		public Task<U> SwitchAsync<U>(Func<T, Task<U>> some, Func<IMsg, Task<U>> none) =>
			F.OptionF.SwitchAsync(this, some: some, none: none);

		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}}?, Func{T, Option{T}}?)"/>
		public Option<T> SwitchIf(Func<T, bool> check, Func<T, Option<T>>? ifTrue = null, Func<T, Option<T>>? ifFalse = null) =>
			F.OptionF.SwitchIf(this, check, ifTrue, ifFalse);

		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, IMsg})"/>
		public Option<T> SwitchIf(Func<T, bool> check, Func<T, IMsg> ifFalse) =>
			F.OptionF.SwitchIf(this, check, ifFalse);

		#endregion

		#region Unwrap

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg, T})"/>
		public T Unwrap(T ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone);

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg, T})"/>
		public T Unwrap(Func<T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone());

		/// <inheritdoc cref="Unwrap{T}(Option{T}, Func{IMsg, T})"/>
		public T Unwrap(Func<IMsg, T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: ifNone);

		/// <inheritdoc cref="UnwrapSingle{T, U}(Option{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
		public Option<U> UnwrapSingle<U>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
			UnwrapSingle<T, U>(this, noItems, tooMany, notAList);

		#endregion
	}
}
