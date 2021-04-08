// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.EnumeratedList_Tests
{
	public class Serialise_Tests
	{
		[Fact]
		public void Empty_List_Returns_Empty_Json()
		{
			// Arrange
			var list = new EnumeratedList<Foo>();

			// Act
			var result = list.Serialise();

			// Assert
			Assert.Equal(F.JsonF.Empty, result);
		}

		[Fact]
		public void List_Returns_Json()
		{
			// Arrange
			var list = new EnumeratedList<Foo> { { Foo.A }, { Foo.B } };
			var json = $"[\"{Foo.A}\",\"{Foo.B}\"]";

			// Act
			var result = list.Serialise();

			// Assert
			Assert.Equal(json, result);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }

			public static readonly Foo A = new(F.Rnd.Str);

			public static readonly Foo B = new(F.Rnd.Str);
		}
	}
}
