// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;
using static Jeebs.Linq.LinqExpressionExtensions.Msg;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class AddWhere<TOptions, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TId>
		where TId : StrongId
	{
		public abstract void Test00_Column_Exists_Adds_Where();

		protected void Test00()
		{
			// Arrange
			var (options, v) = Setup();
			var bar = F.Rnd.Str;
			var cmp = Compare.LessThan;
			var val = F.Rnd.Int;

			// Act
			var result = options.AddWhereTest<TestTable>(v.Parts, new(F.Rnd.Str, bar), c => c.Bar, cmp, val);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(bar, x.column.Name);
					Assert.Equal(cmp, x.cmp);
					Assert.Equal(val, x.value);
				}
			);
		}

		public abstract void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg();

		protected void Test01()
		{
			// Arrange
			var (options, v) = Setup();
			var bar = F.Rnd.Str;
			var cmp = Compare.LessThan;
			var val = F.Rnd.Int;

			// Act
			var result = options.AddWhereTest<TestTable>(v.Parts, new(F.Rnd.Str, F.Rnd.Str), _ => F.Rnd.Str, cmp, val);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<PropertyDoesNotExistOnTypeMsg<TestTable>>(none);
		}

		public sealed record TestTable(string Foo, string Bar) : Table(F.Rnd.Str);
	}
}
