using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public partial class Escape_Tests
	{
		[Fact]
		public void Calls_Escape_For_Each_String_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var c0 = F.Rnd.Str;
			var c1 = F.Rnd.Str;

			// Act
			var result = adapter.Escape(new[] { c0, c1 });

			// Assert
			adapter.Received(2).Escape(Arg.Any<string>());
			adapter.Received().Escape(c0);
			adapter.Received().Escape(c1);
			Assert.Equal(2, result.Count);
		}
	}
}
