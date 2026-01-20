// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.TypeHandlers.Json_Tests;

public class Format_Tests
{
	[Fact]
	public void Null_Sets_Null_Value()
	{
		// Arrange
		var handler = new JsonTypeHandler<Test>();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, null!);

		// Assert
		Assert.IsType<DBNull>(parameter.Value);
	}

	[Fact]
	public void Object_Sets_Value_As_Json()
	{
		// Arrange
		var handler = new JsonTypeHandler<Test>();
		var parameter = Substitute.For<IDbDataParameter>();
		var v0 = Rnd.Str;
		var v1 = Rnd.Int;
		var input = new Test { Foo = v0, Bar = v1 };
		var expected = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"empty\":null}}";

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
