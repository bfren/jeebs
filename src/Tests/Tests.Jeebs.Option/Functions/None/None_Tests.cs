﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class None_Tests
	{
		[Fact]
		public void Returns_None_Without_Reason()
		{
			// Arrange

			// Act
			var result = None<int>(true);

			// Assert
			result.AssertNone();
		}

		[Fact]
		public void Returns_None_With_IfYouArentSureDontMakeItMsg()
		{
			// Arrange

			// Act
			var result = None<int>(false);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<IfYouArentSureDontMakeItMsg>(none);
		}

		[Fact]
		public void Returns_None_With_Reason_Object()
		{
			// Arrange
			var reason = Substitute.For<IMsg>();

			// Act
			var result = None<int>(reason);

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
		}

		[Fact]
		public void Returns_None_With_Reason_Type()
		{
			// Arrange

			// Act
			var result = None<int, TestMsg>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TestMsg>(none);
		}

		public record TestMsg : IMsg { }
	}
}