// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;

public abstract class GetColumns_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IUnion, new()
{
	public abstract void Test00_Calls_Extract_From();

	protected void Test00<TModel>()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		builder.GetColumns<TModel>();

		// Assert
		v.Extract.Received().From<TModel>(Arg.Any<ITable[]>());
	}
}
