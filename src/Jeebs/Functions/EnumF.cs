// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using static F.OptionF;

namespace F
{
	/// <summary>
	/// Enum functions
	/// </summary>
	public static class EnumF
	{
		/// <summary>
		/// Parse a string value into the specified Enum
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="value">The value to parse</param>
		/// <returns>Parsed value</returns>
		public static Option<T> Parse<T>(string value)
			where T : struct, Enum
		{
			try
			{
				return Enum.Parse(typeof(T), value) switch
				{
					T x =>
						x,

					_ =>
						None<T>(new Msg.NotAValidEnumValueMsg<T>(value))
				};
			}
			catch (Exception)
			{
				return None<T>(new Msg.NotAValidEnumValueMsg<T>(value));
			}
		}

		/// <summary>
		/// Parse a string value into the specified Enum
		/// </summary>
		/// <param name="t">Enum type</param>
		/// <param name="value">The value to parse</param>
		/// <returns>Parsed value</returns>
		public static Option<object> Parse(Type t, string value)
		{
			if (!t.IsEnum)
			{
				return None<object>(new Msg.NotAValidEnumMsg(t));
			}

			try
			{
				return Enum.Parse(t, value, true);
			}
			catch (Exception)
			{
				return None<object>(new Msg.NotAValidEnumValueMsg(t, value));
			}
		}

		/// <summary>
		/// Parse an integer value into the specified Enum
		/// Throws
		///		KeyNotFoundException when the value does not exist in the specified Enum type
		/// </summary>
		/// <typeparam name="TFrom">Enum type</typeparam>
		/// <param name="value">The value to parse</param>
		/// <returns>Parsed value</returns>
		public static FluentConvert<TFrom> Convert<TFrom>(TFrom value)
			where TFrom : struct, Enum =>
			new(value);

		/// <summary>
		/// FluentConvert
		/// </summary>
		/// <typeparam name="TFrom">Convert from type</typeparam>
		public sealed class FluentConvert<TFrom>
			where TFrom : struct, Enum
		{
			private readonly TFrom from;

			/// <summary>
			/// Construct object
			/// </summary>
			/// <param name="from">Convert from type</param>
			public FluentConvert(TFrom from) =>
				this.from = from;

			/// <summary>
			/// Convert value to specified type
			/// </summary>
			/// <typeparam name="TTo">Convert To type</typeparam>
			/// <returns>Converted object</returns>
			public Option<TTo> To<TTo>()
				where TTo : struct, Enum
			{
				// Convert to long so we can get the value of the receiving enum
				var fromLong = System.Convert.ChangeType(from, typeof(long));

				// Convert to receiving Enum - if fromLong is not defined in TTo, Enum.ToObject() will return
				// fromLong, rather than the Enum value, so we also need to check the parsed object exists in
				// TTo before returning it
				return Enum.ToObject(typeof(TTo), fromLong) switch
				{
					TTo x when Enum.IsDefined(typeof(TTo), x) =>
						x,

					_ =>
						None<TTo>(new Msg.ValueNotInReceivingEnumMsg<TFrom, TTo>(from))
				};
			}
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary><paramref name="Value"/> Type is not a valid <see cref="Enum"/></summary>
			/// <param name="Value">Enum type</param>
			public sealed record NotAValidEnumMsg(Type Value) : WithValueMsg<Type> { }

			/// <summary><paramref name="Value"/> is not a valid value of <typeparamref name="T"/></summary>
			/// <typeparam name="T">Enum type</typeparam>
			/// <param name="Value">Enum value</param>
			public sealed record NotAValidEnumValueMsg<T>(string Value) : WithValueMsg<string>
				where T : struct, Enum
			{
				/// <summary>Return message</summary>
				public override string ToString() =>
					$"'{Value}' is not a valid value of {typeof(T)}.";
			}

			/// <summary><paramref name="Value"/> is not a valid value of <paramref name="Type"/></summary>
			/// <param name="Type">Enum type</param>
			/// <param name="Value">Enum value</param>
			public sealed record NotAValidEnumValueMsg(Type Type, string Value) : WithValueMsg<string>
			{
				/// <summary>Return message</summary>
				public override string ToString() =>
					$"'{Value}' is not a valid value of {Type}.";
			}

			/// <summary><paramref name="Value"/> is not in <typeparamref name="TTo"/></summary>
			/// <typeparam name="TFrom">From Enum</typeparam>
			/// <typeparam name="TTo">To Enum</typeparam>
			/// <param name="Value">From Enum value</param>
			public sealed record ValueNotInReceivingEnumMsg<TFrom, TTo>(TFrom Value) : WithValueMsg<TFrom>
				where TFrom : struct, Enum
				where TTo : struct, Enum
			{
				/// <summary>Return message</summary>
				public override string ToString() =>
					$"'{Value}' is not a valid {typeof(TTo)}.";
			}
		}
	}
}
