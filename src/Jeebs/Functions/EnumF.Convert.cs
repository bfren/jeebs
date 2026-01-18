// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

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
	/// <remarks>
	/// Construct object
	/// </remarks>
	/// <param name="from">Convert from type</param>
	public sealed class FluentConvert<TFrom>(TFrom from)
		where TFrom : struct, Enum
	{
		private readonly TFrom from = from;

		/// <summary>
		/// Convert value to specified type
		/// </summary>
		/// <typeparam name="TTo">Convert To type</typeparam>
		public Result<TTo> To<TTo>()
			where TTo : struct, Enum
		{
			// Convert to long so we can get the value of the receiving enum
			var fromLong = System.Convert.ChangeType(from, typeof(long), CultureInfo.InvariantCulture);

			// Convert to receiving Enum - if fromLong is not defined in TTo, Enum.ToObject() will return
			// fromLong, rather than the Enum value, so we also need to check the parsed object exists in
			// TTo before returning it
			return Enum.ToObject(typeof(TTo), fromLong) switch
			{
				TTo x when Enum.IsDefined(x) =>
					x,

				_ =>
					FailNotAValidEnumValue<TTo>(fromLong.ToString()),
			};
		}
	}
}
