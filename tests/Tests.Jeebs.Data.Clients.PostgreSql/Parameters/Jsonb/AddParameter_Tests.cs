// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Npgsql;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.PostgreSql.Parameters.Jsonb_Tests
{
	public class AddParameter_Tests
	{
		[Fact]
		public void Adds_Parameter_As_Jsonb()
		{
			// Arrange
			var value = F.Rnd.Str;
			var name = F.Rnd.Str;
			var param = new Jsonb(value);

			var collection = Substitute.For<IDataParameterCollection>();
			var command = Substitute.For<IDbCommand>();
			command.Parameters.Returns(collection);

			// Act
			param.AddParameter(command, name);

			// Assert
			collection.Received().Add(Arg.Is<NpgsqlParameter>(p => p.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Jsonb));
		}

		[Fact]
		public void Adds_Value()
		{
			// Arrange
			var value = F.Rnd.Str;
			var name = F.Rnd.Str;
			var param = new Jsonb(value);

			var collection = Substitute.For<IDataParameterCollection>();
			var command = Substitute.For<IDbCommand>();
			command.Parameters.Returns(collection);

			// Act
			param.AddParameter(command, name);

			// Assert
			collection.Received().Add(Arg.Is<NpgsqlParameter>(p => p.Value == (object)value));
		}
	}
}
