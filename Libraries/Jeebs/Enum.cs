using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Custom enum
	/// </summary>
	public abstract class Enum
	{
		/// <summary>
		/// The string representation ('name' in Enum terms) of this enum value
		/// </summary>
		private readonly string name;

		/// <summary>
		/// Set the name of this value
		/// </summary>
		/// <param name="name">Value name</param>
		protected Enum(string name) => this.name = name;

		/// <summary>
		/// Return the name of this value
		/// </summary>
		/// <returns>Name of this value</returns>
		public override string ToString() => name;

		#region Static Members

		/// <summary>
		/// Thread-safe parser cache
		/// </summary>
		private static readonly ConcurrentDictionary<string, object> cache;

		/// <summary>
		/// Create cache object
		/// </summary>
		static Enum() => cache = new ConcurrentDictionary<string, object>();

		/// <summary>
		/// Check whether or not the specified name matches the given value
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="value">Enum value</param>
		private static Result<T> Check<T>(string name, T value)
			where T : Enum
		{
			if (string.Equals(value.ToString(), name, StringComparison.OrdinalIgnoreCase))
			{
				return Result.Success(value);
			}

			return Result.Failure<T>($"'{value}' does not match '{name}'.");
		}

		/// <summary>
		/// Parse a given name and return the correct Enum value - or throw an exception if no match is found
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="values">Enum values to check name against</param>
		/// <exception cref="Jx.ParseException">If string <paramref name="name"/> is not a valid value of Enum type <typeparamref name="T"/>.</exception>
		/// <returns>Matching Enum value, or throws an exception if no match was found</returns>
		protected static T Parse<T>(string name, T[] values)
			where T : Enum
		{
			// Return the Enum value
			return (T)cache.GetOrAdd(
				$"{typeof(T)}-{name}",
				(_, args) =>
				{
					// Check all given values against name
					foreach (var item in args!.Values)
					{
						if (Check(args.Name, item) is Success<T> success)
						{
							return success.Val;
						}
					}

					// If we get here the name was never matched
					throw new Jx.ParseException($"'{args.Name}' is not a valid value of '{typeof(T)}'.");
				},
				new ParseArgs<T>(name, values)
			);
		}

		/// <summary>
		/// Parse Arguments
		/// </summary>
		/// <typeparam name="T">Enum Type</typeparam>
		private class ParseArgs<T>
			where T : Enum
		{
			/// <summary>
			/// Enum name to parse
			/// </summary>
			public readonly string Name;

			/// <summary>
			/// Enum values to test Name against
			/// </summary>
			public readonly T[] Values;

			public ParseArgs(string name, T[] values)
			{
				Name = name;
				Values = values;
			}
		}

		#endregion
	}
}
