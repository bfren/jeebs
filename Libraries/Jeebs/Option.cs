using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Create option types
	/// </summary>
	public static class Option
	{
		/// <summary>
		/// Create a Some option, containing a value
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		public static Some<T> Some<T>(T value)
			=> new Some<T>(value);

		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		public static None<T> None<T>()
			=> new None<T>();
	}

	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract class Option<T>
	{
		internal Option() { }

		/// <summary>
		/// True if this Option is <see cref="Some{T}"/>
		/// </summary>
		public bool IsSome
		{
			get => this is Some<T>;
		}

		/// <summary>
		/// True if this Option is <see cref="None{T}"/>
		/// </summary>
		public bool IsNone
		{
			get => this is None<T>;
		}

		public TReturn Either<TReturn>(Func<T, TReturn> some, Func<TReturn> none)
			=> this switch
			{
				Some<T> s => some(s.Value),
				None<T> _ => none(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};

		/// <summary>
		/// Get the value of this option
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone)
			=> this switch
			{
				Some<T> s => s.Value,
				None<T> _ => ifNone(),
				_ => throw new Exception() // as Option<T> is internal implementation only this should never happen...
			};
	}
}
