// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Functions;

public static partial class DataF
{
	/// <summary>
	/// Get a parameter value - if it's a <see cref="IMonad"/>, return <see cref="IMonad.Value"/>.
	/// </summary>
	/// <param name="value">Parameter Value.</param>
	public static dynamic GetParameterValue(dynamic value) =>
		value switch
		{
			IMonad id =>
				id.Value ?? new object(),

			{ } x =>
				x,

			_ =>
				new object()
		};
}
