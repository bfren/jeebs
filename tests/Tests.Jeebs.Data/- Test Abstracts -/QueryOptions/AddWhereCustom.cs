// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryOptions.Msg;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class AddWhereCustom<TOptions, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TId>
		where TId : StrongId
	{
		public abstract void Test00_Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input);

		protected void Test00(string input)
		{
			// Arrange
			var (options, v) = Setup();

			// Act
			var result = options.AddWhereCustomTest(v.Parts, input, new());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TryingToAddEmptyClauseToWhereCustomMsg>(none);
		}

		public abstract void Test01_Invalid_Parameters_Returns_None_With_UnableToAddParametersToWhereCustomMsg(object input);

		protected void Test01(object input)
		{
			// Arrange
			var (options, v) = Setup();
			var clause = F.Rnd.Str;

			// Act
			var result = options.AddWhereCustomTest(v.Parts, clause, input);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToAddParametersToWhereCustomMsg>(none);
		}

		public abstract void Test02_Returns_New_Parts_With_Clause_And_Parameters();

		protected void Test02()
		{
			// Arrange
			var (options, v) = Setup();
			var clause = F.Rnd.Str;
			var value = F.Rnd.Lng;
			var parameters = new { value };

			// Act
			var result = options.AddWhereCustomTest(v.Parts, clause, parameters);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.WhereCustom,
				x =>
				{
					Assert.Equal(clause, x.clause);
					Assert.Collection(x.parameters,
						y =>
						{
							Assert.Equal(nameof(value), y.Key);
							Assert.Equal(value, y.Value);
						}
					);
				}
			);
		}
	}
}
