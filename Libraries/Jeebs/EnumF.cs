// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static JeebsF.OptionF;

namespace JeebsF
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
						None<T>(new Jm.Functions.EnumF.NotAValidEnumValueMsg<T>(value))
				};
			}
			catch (Exception)
			{
				return None<T>(new Jm.Functions.EnumF.NotAValidEnumValueMsg<T>(value));
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
				return None<object>(new Jm.Functions.EnumF.NotAValidEnumMsg(t));
			}

			try
			{
				var parsed = Enum.Parse(t, value, true);
				return Return(parsed);
			}
			catch (Exception)
			{
				return None<object>(new Jm.Functions.EnumF.NotAValidEnumValueMsg(t, value));
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
						None<TTo>(new Jm.Functions.EnumF.ValueNotInReceivingEnumMsg<TFrom, TTo>(from))
				};
			}
		}
	}
}
