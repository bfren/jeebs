// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			var (builder, v) = Setup<TModel>();

			// Act
			var result = builder.Create<TModel>(null, F.Rnd.Lng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Same(builder.Table, result.From);
		}

		public abstract void Test01_Returns_With_Select();

		protected void Test01()
		{
			// Arrange
			var (builder, v) = Setup<TModel>();

			// Act
			var result = builder.Create<TModel>(null, F.Rnd.Lng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Same(v.Columns, result.Select);
		}

		public abstract void Test02_Returns_With_Maximum();

		protected void Test02()
		{
			// Arrange
			var maximum = F.Rnd.Lng;
			var (builder, v) = Setup<TModel>();

			// Act
			var result = builder.Create<TModel>(maximum, F.Rnd.Lng);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Equal(maximum, result.Maximum);
		}

		public abstract void Test03_Returns_With_Skip();

		protected void Test03()
		{
			// Arrange
			var skip = F.Rnd.Lng;
			var (builder, v) = Setup<TModel>();

			// Act
			var result = builder.Create<TModel>(null, skip);

			// Assert
			Assert.NotSame(v.Parts, result);
			Assert.Equal(skip, result.Skip);
		}
	}
}
