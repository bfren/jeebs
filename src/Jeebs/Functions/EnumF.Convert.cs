// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using Jeebs.Messages;

namespace Jeebs.Functions;

public static partial class EnumF
{
	/// <summary>
	/// Parse an integer value into the specified Enum
	/// Throws
	///		KeyNotFoundException when the value does not exist in the specified Enum type
	/// </summary>
	/// <typeparam name="TFrom">Enum type</typeparam>
	/// <param name="value">The value to parse</param>
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
		public Maybe<TTo> To<TTo>()
			where TTo : struct, Enum
		{
			// Convert to long so we can get the value of the receiving enum
			var fromLong = System.Convert.ChangeType(from, typeof(long), CultureInfo.InvariantCulture);

			// Convert to receiving Enum - if fromLong is not defined in TTo, Enum.ToObject() will return
			// fromLong, rather than the Enum value, so we also need to check the parsed object exists in
			// TTo before returning it
			return Enum.ToObject(typeof(TTo), fromLong) switch
			{
				TTo x when Enum.IsDefined(typeof(TTo), x) =>
					x,

				_ =>
					F.None<TTo>(new M.ValueNotInReceivingEnumMsg<TFrom, TTo>(from))
			};
		}
	}

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary><paramref name="Value"/> Type is not a valid <see cref="Enum"/></summary>
		/// <param name="Value">Enum type</param>
		public sealed record class NotAValidEnumMsg(Type Value) : WithValueMsg<Type>;

		/// <summary>Attempting to parse a null value</summary>
		public sealed record class NullValueMsg : Msg;

		/// <summary><paramref name="Value"/> is not a valid value of <typeparamref name="T"/></summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="Value">Enum value</param>
		public sealed record class NotAValidEnumValueMsg<T>(string Value) : Msg
			where T : struct, Enum
		{
			/// <inheritdoc/>
			public override string Format =>
				"'{Value}' is not a valid value of {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { Value, typeof(T) };
		}

		/// <summary><paramref name="Value"/> is not a valid value of <paramref name="EnumType"/></summary>
		/// <param name="EnumType">Enum type</param>
		/// <param name="Value">Enum value</param>
		public sealed record class NotAValidEnumValueMsg(Type EnumType, string Value) : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"'{Value}' is not a valid value of {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { Value, EnumType };
		}

		/// <summary><paramref name="Value"/> is not in <typeparamref name="TTo"/></summary>
		/// <typeparam name="TFrom">From Enum</typeparam>
		/// <typeparam name="TTo">To Enum</typeparam>
		/// <param name="Value">From Enum value</param>
		public sealed record class ValueNotInReceivingEnumMsg<TFrom, TTo>(TFrom Value) : Msg
			where TFrom : struct, Enum
			where TTo : struct, Enum
		{
			/// <inheritdoc/>
			public override string Format =>
				"'{Value}' is not a valid {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { Value, typeof(TTo) };
		}
	}
}
