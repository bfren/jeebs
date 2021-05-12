﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder.Msg;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddWhereCustom_Tests : QueryPartsBuilder_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input)
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereCustom(v.Parts, input, new());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TryingToAddEmptyClauseToWhereCustomMsg>(none);
		}

		[Theory]
		[InlineData(null)]
		[InlineData(42)]
		[InlineData(true)]
		[InlineData('c')]
		public void Invalid_Parameters_Returns_None_With_UnableToAddParametersToWhereCustomMsg(object input)
		{
			// Arrange
			var (builder, v) = Setup();
			var clause = F.Rnd.Str;

			// Act
			var result = builder.AddWhereCustom(v.Parts, clause, input);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnableToAddParametersToWhereCustomMsg>(none);
		}

		[Fact]
		public void Returns_New_Parts_With_Clause_And_Parameters()
		{
			// Arrange
			var (builder, v) = Setup();
			var clause = F.Rnd.Str;
			var value = F.Rnd.Lng;
			var parameters = new { value };

			// Act
			var result = builder.AddWhereCustom(v.Parts, clause, parameters);

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
