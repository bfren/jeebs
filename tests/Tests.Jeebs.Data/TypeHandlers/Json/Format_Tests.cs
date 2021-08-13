// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Json_Tests
{
	public class Format_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_Sets_Null_Value(Test input)
		{
			// Arrange
			var handler = new JsonTypeHandler<Test>();
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, input);

			// Assert
			Assert.IsType<DBNull>(parameter.Value);
		}

		[Fact]
		public void Object_Sets_Value_As_Json()
		{
			// Arrange
			var handler = new JsonTypeHandler<Test>();
			var parameter = Substitute.For<IDbDataParameter>();
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Int;
			var input = new Test { Foo = v0, Bar = v1 };
			var expected = $"{{\"foo\":\"{v0}\",\"bar\":{v1}}}";

			// Act
			handler.SetValue(parameter, input);

			// Assert
			Assert.Equal(expected, parameter.Value);
		}

		public class Test
		{
			public string Foo { get; set; } = string.Empty;

			public int Bar { get; set; }

			public string? Empty { get; set; }
		}
	}
}
