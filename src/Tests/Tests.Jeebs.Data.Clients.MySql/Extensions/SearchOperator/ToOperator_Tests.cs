// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Xunit;

namespace Jeebs.Data.Clients.MySql.SearchOperatorExtensions_Tests
{
	public class ToOperator_Tests
	{
		[Fact]
		public void Equal_Returns_Equals_Sign()
		{
			// Arrange
			var value = SearchOperator.Equal;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("=", result);
		}

		[Fact]
		public void Like_Returns_Like()
		{
			// Arrange
			var value = SearchOperator.Like;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("LIKE", result);
		}

		[Fact]
		public void NotEqual_Returns_NotEquals_Sign()
		{
			// Arrange
			var value = SearchOperator.NotEqual;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("!=", result);
		}

		[Fact]
		public void LessThan_Returns_LessThan_Sign()
		{
			// Arrange
			var value = SearchOperator.LessThan;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("<", result);
		}

		[Fact]
		public void LessThanOrEqual_Returns_LessThanOrEqual_Sign()
		{
			// Arrange
			var value = SearchOperator.LessThanOrEqual;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("<=", result);
		}

		[Fact]
		public void MoreThan_Returns_MoreThan_Sign()
		{
			// Arrange
			var value = SearchOperator.MoreThan;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal(">", result);
		}

		[Fact]
		public void MoreThanOrEqual_Returns_MoreThanOrEqual_Sign()
		{
			// Arrange
			var value = SearchOperator.MoreThanOrEqual;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal(">=", result);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(10)]
		[InlineData(20)]
		public void Other_Returns_Equals_Sign(int input)
		{
			// Arrange
			var value = (SearchOperator)input;

			// Act
			var result = value.ToOperator();

			// Assert
			Assert.Equal("=", result);
		}
	}
}
