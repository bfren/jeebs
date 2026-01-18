// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Get a parameter value - if it's a <see cref="IStrongId"/>, return <see cref="IStrongId.Value"/>.
	/// </summary>
	/// <param name="value">Parameter Value.</param>
	public static dynamic GetParameterValue(dynamic value) =>
		value switch
		{
			IStrongId id =>
				id.Value,

			{ } x =>
				x,

			_ =>
				new object()
		};
}
