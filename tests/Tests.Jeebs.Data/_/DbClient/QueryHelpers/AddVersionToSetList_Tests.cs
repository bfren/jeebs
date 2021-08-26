// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
	public class AddVersionToSetList_Tests
	{
		[Fact]
		public void VersionColumn_Null_Does_Nothing()
		{
			// Arrange
			var client = Substitute.ForPartsOf<DbClient>();
			var set = new List<string>();

			// Act
			client.AddVersionToSetListTest(set, null);

			// Assert
			Assert.Empty(set);
		}

		[Fact]
		public void Adds_VersionColumn_To_Columns()
		{
			// Arrange
			var client = Substitute.ForPartsOf<DbClient>();
			client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).Name}--");
			client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var version = new Column(F.Rnd.Str, name, alias);
			var expected = $"--{name}-- = ##{alias}## + 1";

			var set = new List<string>();

			// Act
			client.AddVersionToSetListTest(set, version);

			// Assert
			Assert.Collection(set,
				x => Assert.Equal(expected, x)
			);
		}
	}
}
