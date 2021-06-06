// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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

		public sealed record TestId() : StrongId(0);
	}
}
