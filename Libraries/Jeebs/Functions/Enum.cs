using EnumsNET;
using System;

namespace F
{
	/// <summary>
	/// Enum functions
	/// </summary>
	public static class EnumF
	{
		/// <summary>
		/// Parse a string value into the specified Enum
		/// Throws
		///		KeyNotFoundException when the value does not exist in the specified Enum type
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="value">The value to parse</param>
		/// <returns>Parsed value</returns>
		public static T Parse<T>(string value) where T : struct, Enum
		{
			try
			{
				return Enums.Parse<T>(value);
			}
			catch (Exception)
			{
				throw new Jx.ParseException($"'{value}' is not a valid value of '{typeof(T).FullName}'.");
			}
		}

		/// <summary>
		/// Parse a string value into the specified Enum
		/// Throws
		///		KeyNotFoundException when the value does not exist in the specified Enum type
		/// </summary>
		/// <param name="t">Enum type</param>
		/// <param name="value">The value to parse</param>
		/// <returns>Parsed value</returns>
		public static object Parse(Type t, string value)
		{
			if (!t.IsEnum)
			{
				throw new ArgumentException($"Type {t} is not an Enum", nameof(t));
			}

			try
			{
				return Enum.Parse(t, value, true);
			}
			catch (Exception)
			{
				throw new Jx.ParseException($"'{value}' is not a valid value of '{t.FullName}'.");
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
		public static FluentConvert<TFrom> Convert<TFrom>(TFrom value) where TFrom : struct, Enum => new FluentConvert<TFrom>(value);

		/// <summary>
		/// FluentConvert
		/// </summary>
		/// <typeparam name="TFrom">Convert from type</typeparam>
		public sealed class FluentConvert<TFrom> where TFrom : struct, Enum
		{
			private readonly TFrom from;

			/// <summary>
			/// Construct object
			/// </summary>
			/// <param name="from">Convert from type</param>
			public FluentConvert(TFrom from) => this.from = from;

			/// <summary>
			/// Convert value to specified type
			/// </summary>
			/// <typeparam name="TTo">Convert To type</typeparam>
			/// <returns>Converted object</returns>
			public TTo To<TTo>() where TTo : struct, Enum
			{
				var fromInt = Enums.ToInt32(from);
				if (Enums.TryToObject(fromInt, out TTo converted) && Enum.IsDefined(typeof(TTo), converted))
				{
					return converted;
				}
				else
				{
					throw new Jx.ParseException($"'{from}' is not a valid value of '{typeof(TTo).FullName}'.");
				}
			}
		}
	}
}
