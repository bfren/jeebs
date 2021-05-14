// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
	public class AddVersionToWhere_Tests
	{
		[Fact]
		public void VersionColumn_Null_Does_Nothing()
		{
			// Arrange
			var client = Substitute.ForPartsOf<DbClient>();
			var sql = new StringBuilder();

			// Act
			client.AddVersionToWhereTest(sql, null);

			// Assert
			Assert.Equal(string.Empty, sql.ToString());
		}

		[Fact]
		public void Adds_Version_To_Where()
		{
			// Arrange
			var client = Substitute.ForPartsOf<DbClient>();
			client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).Name}--");
			client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var version = new Column(F.Rnd.Str, name, alias);
			var expected = $"--{name}-- = ##{alias}##";

			var sql = new StringBuilder();

			// Act
			client.AddVersionToWhereTest(sql, version);

			// Assert
			Assert.Equal(expected, sql.ToString());
		}

		[Fact]
		public void Adds_And_Version_To_Where()
		{
			// Arrange
			var client = Substitute.ForPartsOf<DbClient>();
			client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).Name}--");
			client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var version = new Column(F.Rnd.Str, name, alias);

			var query = F.Rnd.Str;
			var sql = new StringBuilder(query);

			var expected = $"{query} AND --{name}-- = ##{alias}##";

			// Act
			client.AddVersionToWhereTest(sql, version);

			// Assert
			Assert.Equal(expected, sql.ToString());
		}
	}
}
