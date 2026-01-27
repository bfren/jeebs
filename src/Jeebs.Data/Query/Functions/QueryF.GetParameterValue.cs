// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Get a parameter value - if it's a <see cref="IUnion"/>, return <see cref="IUnion.Value"/>.
	/// </summary>
	/// <param name="value">Parameter Value.</param>
	public static dynamic GetParameterValue(dynamic value) =>
		value switch
		{
			IUnion id =>
				id.Value ?? new object(),

			{ } x =>
				x,

			_ =>
				new object()
		};
}
