// Mileage Tracker: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2022

using Jeebs.Data.Query;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.CountAsync"/>
	/// </summary>
	/// <param name="call">Call</param>
	public static void AssertCount(ICall call)
	{
		// Check the method name
		AssertMethodName(call, nameof(IFake.CountAsync));
	}
}
