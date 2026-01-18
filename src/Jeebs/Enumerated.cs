// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Concurrent;

namespace Jeebs;

/// <summary>
/// Enables custom enumerated values.
/// </summary>
public abstract record class Enumerated : IEquatable<Enumerated>, IEquatable<string>
{
	/// <summary>
	/// The string representation ('name' in Enum terms) of this enumerated value.
	/// </summary>
	private readonly string name;

	/// <summary>
	/// Set the name of this value, allowing empty values.
	/// </summary>
	/// <param name="name">Value name.</param>
	protected Enumerated(string name) : this(name, true) { }

	/// <summary>
	/// Set the name of this value.
	/// </summary>
	/// <param name="name">Value name.</param>
	/// <param name="allowEmpty">If <see langword="false"/>, and <paramref name="name"/> is null or empty, will throw an exception.</param>
	protected Enumerated(string name, bool allowEmpty)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			if (allowEmpty)
			{
				this.name = string.Empty;
			}
			else
			{
				throw new ArgumentNullException(nameof(name));
			}
		}
		else
		{
			this.name = name.Trim();
		}
	}

	/// <summary>
	/// Return the name of this value.
	/// </summary>
	/// <returns>Name of this value</returns>
	public sealed override string ToString() =>
		name;

	#region Static Members

	/// <summary>
	/// Thread-safe parser cache.
	/// </summary>
	private static ConcurrentDictionary<string, object> Cache { get; }

	/// <summary>
	/// Create cache object.
	/// </summary>
	static Enumerated() =>
		Cache = new ConcurrentDictionary<string, object>();

	/// <summary>
	/// Check whether or not the specified name matches the given value.
	/// </summary>
	/// <typeparam name="T">Enum value type</typeparam>
	/// <param name="name">Enum name.</param>
	/// <param name="value">Enum value.</param>
	internal static Result<T> Check<T>(string name, T value)
		where T : Enumerated =>
		string.Equals(value.ToString(), name, StringComparison.OrdinalIgnoreCase) switch
		{
			true =>
				value,

			false =>
				R.Fail("'{Value}' is not a valid value of {Type}.", value, typeof(T))
		};

	/// <summary>
	/// Parse a given name and return the correct Enum value - or throw an exception if no match is found.
	/// </summary>
	/// <typeparam name="T">Enum value type</typeparam>
	/// <param name="name">Enum name.</param>
	/// <param name="values">Enum values to check name against.</param>
	protected static Result<T> Parse<T>(string name, T[] values)
		where T : Enumerated =>
		(Result<T>)Cache.GetOrAdd(
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
				return R.Fail("'{Value}' is not a valid value of {Type}.", name, typeof(T));
			},
			new ParseArgs<T>(name, values)
		);

	/// <summary>
	/// Returns true if the given name matches a registered Enum value.
	/// </summary>
	/// <typeparam name="T">Enum value type</typeparam>
	/// <param name="name">Enum name.</param>
	/// <param name="values">Enum values to check name against.</param>
	protected static bool IsRegistered<T>(string name, T[] values)
		where T : Enumerated =>
		Parse(name, values).IsOk;

	/// <summary>
	/// Parse Arguments.
	/// </summary>
	/// <typeparam name="T">Enum Type</typeparam>
	private sealed class ParseArgs<T>
		where T : Enumerated
	{
		/// <summary>
		/// Enum name to parse.
		/// </summary>
		public string Name { get; private init; }

		/// <summary>
		/// Enum values to test Name against.
		/// </summary>
		public T[] Values { get; private init; }

		/// <summary>
		/// Create object.
		/// </summary>
		/// <param name="name">Enumerated name to parse.</param>
		/// <param name="values">Enumerated values to check <paramref name="name"/> against.</param>
		public ParseArgs(string name, T[] values) =>
			(Name, Values) = (name, values);
	}

	#endregion Static Members

	#region Operators

	/// <summary>
	/// Allow implicit conversion to string.
	/// </summary>
	/// <param name="e">Enumerated value.</param>
	public static implicit operator string(Enumerated e) =>
		e.ToString();

	/// <summary>
	/// Compare an enumerated type with a value type
	/// <para>The name of <paramref name="l"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Enumerated.</param>
	/// <param name="r">Value.</param>
	public static bool operator ==(Enumerated l, string r) =>
		l.Equals(r);

	/// <summary>
	/// Compare an enumerated type with a value type
	/// <para>The name of <paramref name="l"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value.</param>
	/// <param name="r">Enumerated.</param>
	public static bool operator ==(string l, Enumerated r) =>
		r.Equals(l);

	/// <summary>
	/// Compare an enumerated type with a value type
	/// <para>The name of <paramref name="l"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Enumerated.</param>
	/// <param name="r">Value.</param>
	public static bool operator !=(Enumerated l, string r) =>
		!l.Equals(r);

	/// <summary>
	/// Compare an enumerated type with a value type
	/// <para>The name of <paramref name="l"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value.</param>
	/// <param name="r">Enumerated.</param>
	public static bool operator !=(string l, Enumerated r) =>
		!r.Equals(l);

	#endregion Operators

	#region Overrides

	/// <summary>
	/// Compare this <see cref="Enumerated"/> with another object
	/// <para>Each <see cref="name"/> and type will be compared</para>
	/// </summary>
	/// <param name="other">Object to compare to this <see cref="Enumerated"/>.</param>
	public virtual bool Equals(Enumerated? other) =>
		(name == other?.name) && (GetType().FullName == other?.GetType().FullName);

	/// <summary>
	/// Compare this <see cref="Enumerated"/> with a string.
	/// </summary>
	/// <param name="other">String to compare to this <see cref="Enumerated"/>.</param>
	public virtual bool Equals(string? other) =>
		name == other;

	/// <summary>
	/// Generate custom HashCode.
	/// </summary>
	public override int GetHashCode() =>
		GetType().GetHashCode() ^ name.GetHashCode();

	/// <inheritdoc cref="GetHashCode()"/>
	public int GetHashCode(IEqualityComparer comparer) =>
		GetType().GetHashCode() ^ comparer.GetHashCode(name);

	#endregion Overrides
}
