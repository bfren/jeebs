// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.StrongId_Tests
{
	public class SetValue_Tests
	{
		[Fact]
		public void Sets_Parameter_Value_As_Int64()
		{
			// Arrange
			var handler = new StrongIdTypeHandler<TestId>();
			var value = F.Rnd.Lng;
			var id = new TestId { Value = value };
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, id);

			// Assert
			Assert.Equal(parameter.Value, value);
		}

		public sealed record TestId : StrongId;
	}
}
