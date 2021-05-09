// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class GetMap<TOptions, TEntity, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TEntity, TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		public abstract void Test00_Returns_Entity_Table_And_IdColumn();

		protected void Test00()
		{
			// Arrange
			var (options, v) = Setup();

			// Act
			var result = options.GetMapTest();

			// Assert
			var (table, idColumn) = result.AssertSome();
			Assert.Same(v.Table, table);
			Assert.Same(v.IdColumn, idColumn);
		}
	}
}
