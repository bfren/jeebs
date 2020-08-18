using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Jm.Enum;

namespace Jeebs
{
	/// <summary>
	/// Enables custom enumerated values
	/// </summary>
	public abstract class Enumerated
	{
		/// <summary>
		/// The string representation ('name' in Enum terms) of this enumerated value
		/// </summary>
		private readonly string name;

		/// <summary>
		/// Set the name of this value
		/// </summary>
		/// <param name="name">Value name</param>
		protected Enumerated(string name)
			=> this.name = name;

		/// <summary>
		/// Return the name of this value
		/// </summary>
		/// <returns>Name of this value</returns>
		public override string ToString()
			=> name;

		#region Static Members

		/// <summary>
		/// Thread-safe parser cache
		/// </summary>
		private static readonly ConcurrentDictionary<string, object> cache;

		/// <summary>
		/// Create cache object
		/// </summary>
		static Enumerated()
			=> cache = new ConcurrentDictionary<string, object>();

		/// <summary>
		/// Check whether or not the specified name matches the given value
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="value">Enum value</param>
		private static Option<T> Check<T>(string name, T value)
			where T : Enumerated
			=> string.Equals(value.ToString(), name, StringComparison.OrdinalIgnoreCase) switch
			{
				true => value,
				false => Option.None<T>()
			};

		/// <summary>
		/// Parse a given name and return the correct Enum value - or throw an exception if no match is found
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="values">Enum values to check name against</param>
		/// <returns>Matching Enum value, or throws an exception if no match was found</returns>
		protected static Option<T> Parse<T>(string name, T[] values)
			where T : Enumerated
			=> (Option<T>)cache.GetOrAdd(
				$"{typeof(T)}-{name}",
				(_, args) =>
				{
					// Check all given values against name
					foreach (var item in args.Values)
					{
						if (Check(args.Name, item) is Some<T> s)
						{
							return s;
						}
					}

					// If we get here the name was never matched
					return Option.None<T>().AddReason(new NotAValidEnumValueMsg<T>(name));
				},
				new ParseArgs<T>(name, values)
			);

		/// <summary>
		/// Returns true if the given name matches a registered Enum value
		/// </summary>
		/// <typeparam name="T">Enum value type</typeparam>
		/// <param name="name">Enum name</param>
		/// <param name="values">Enum values to check name against</param>
		protected static bool IsRegistered<T>(string name, T[] values)
			where T : Enumerated
			=> Parse(name, values) is Some<T>;

		/// <summary>
		/// Parse Arguments
		/// </summary>
		/// <typeparam name="T">Enum Type</typeparam>
		private class ParseArgs<T>
			where T : Enumerated
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
				=> (Name, Values) = (name, values);
		}

		#endregion
	}
}
