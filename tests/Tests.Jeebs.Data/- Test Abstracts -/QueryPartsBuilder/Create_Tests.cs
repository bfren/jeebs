// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class Create_Tests<TBuilder, TId, TModel> : QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : StrongId
	{
		public abstract void Test00_Returns_With_Table();

		protected void Test00()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TModel>(null, F.Rnd.Ulng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Same(builder.Table, result.From);
		}

		public abstract void Test01_Calls_Extract_From();

		protected void Test01()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TModel>(null, F.Rnd.Ulng);

			// Assert
			Assert.NotSame(v.Parts, result);
			v.Extract.Received().From<TModel>(Arg.Any<ITable[]>());
		}

		public abstract void Test02_Returns_With_Maximum();

		protected void Test02()
		{
			// Arrange
			var maximum = F.Rnd.Ulng;
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TModel>(maximum, F.Rnd.Ulng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Equal(maximum, result.Maximum);
		}

		public abstract void Test03_Returns_With_Skip();

		protected void Test03()
		{
			// Arrange
			var skip = F.Rnd.Ulng;
			var (builder, v) = Setup();

			// Act
			var result = builder.Create<TModel>(null, skip);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Equal(skip, result.Skip);
		}
	}
}
