// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class GetColumns_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : StrongId
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
}
