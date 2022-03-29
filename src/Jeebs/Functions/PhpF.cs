// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Text;

namespace Jeebs.Functions;

/// <summary>
/// PHP serialisation functions
/// </summary>
public static partial class PhpF
{
	/// <summary>
	/// Array type
	/// </summary>
	public static readonly char ArrayChar = 'a';

	/// <summary>
	/// Boolean type
	/// </summary>
	public static readonly char BooleanChar = 'b';

	/// <summary>
	/// Double type
	/// </summary>
	public static readonly char DoubleChar = 'd';

	/// <summary>
	/// Integer type
	/// </summary>
	public static readonly char IntegerChar = 'i';

	/// <summary>
	/// String type
	/// </summary>
	public static readonly char StringChar = 's';

	/// <summary>
	/// Null type
	/// </summary>
	public static readonly char NullChar = 'N';

	/// <summary>
	/// UTF8Encoding
	/// </summary>
	private static readonly Encoding UTF8 = new UTF8Encoding();

	/// <summary>
	/// Alias for use in deserialising associative arrays
	/// </summary>
	public sealed class AssocArray : Dictionary<object, object> { }
}
