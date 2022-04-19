// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Jeebs.Data.Clients.PostgreSql.TypeHandlers.Json_Tests;

public class Format_Tests
{
	[Theory]
	[InlineData(null)]
	public void Null_Sets_Null_Value(Test input)
	{
		// Arrange
		var handler = new JsonbTypeHandler<Test>();
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
		var handler = new JsonbTypeHandler<Test>();
		var parameter = new NpgsqlParameter();
		var v0 = Rnd.Str;
		var v1 = Rnd.Int;
		var input = new Test { Foo = v0, Bar = v1 };
		var expected = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"empty\":null}}";

		// Act
		handler.SetValue(parameter, input);

		// Assert
		Assert.Equal(expected, parameter.Value);
		Assert.Equal(NpgsqlDbType.Jsonb, parameter.NpgsqlDbType);
	}

	public class Test
	{
		public string Foo { get; set; } = string.Empty;

		public int Bar { get; set; }

		public string? Empty { get; set; }
	}
}
