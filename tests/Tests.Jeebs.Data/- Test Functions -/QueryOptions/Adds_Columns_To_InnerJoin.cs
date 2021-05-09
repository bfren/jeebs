// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Xunit;

namespace F.DataF_Tests
{
	public static partial class QueryOptionsF_Tests
	{
		public static void Adds_Columns_To_InnerJoin<TOptions, TId>(Func<IMapper, TOptions> create)
			where TOptions : QueryOptions<TId>
			where TId : StrongId
		{
			// Arrange
			var (options, v) = Setup<TOptions, TId>(create);

			var t0Name = Rnd.Str;
			var t0Column = Rnd.Str;
			var t0 = new TestTable0(t0Name, t0Column);

			var t1Name = Rnd.Str;
			var t1Column = Rnd.Str;
			var t1 = new TestTable1(t1Name, t1Column);

			// Act
			var result = options.AddInnerJoinTest(v.Parts, t0, t => t.Foo, t1, t => t.Bar);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.InnerJoin,
				x =>
				{
					Assert.Equal(t0Name, x.from.Table);
					Assert.Equal(t0Column, x.from.Name);

					Assert.Equal(t1Name, x.to.Table);
					Assert.Equal(t1Column, x.to.Name);
				}
			);
		}
	}
}
