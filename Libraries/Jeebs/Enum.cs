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
		protected Enum(in string name) => this.name = name;

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
		/// If the specified name matches the given value, return the value - otherwise null
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="value">Enum value</param>
		/// <param name="match">Matched Enum value</param>
		/// <returns>Matching Enum value, or null if there was no match</returns>
		private static IResult<T> Check<T>(in string name, in T value) 
			where T : Enum
		{
			if (string.Equals(value.ToString(), name, StringComparison.OrdinalIgnoreCase))
			{
				return new Success<T>(value);
			}

			return new Failure<T>();
		}

		/// <summary>
		/// Parse a given name and return the correct Enum value - or throw an exception if no match is found
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="values">Enum values to check name against</param>
		/// <exception cref="Jx.ParseException">If string <paramref name="name"/> is not a valid value of Enum type <typeparamref name="T"/>.</exception>
		/// <returns>Matching Enum value, or throws an exception if no match was found</returns>
		protected static T Parse<T>(in string name, in T[] values)
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
							return success.Value;
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

			public ParseArgs(in string name, in T[] values)
			{
				Name = name;
				Values = values;
			}
		}

		#endregion
	}
}
