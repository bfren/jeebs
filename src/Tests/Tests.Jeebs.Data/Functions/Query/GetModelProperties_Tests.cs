// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetModelProperties_Tests
	{
		[Fact]
		public void Returns_Properties_Without_Ignored()
		{
			// Arrange

			// Act
			var result = GetModelProperties<TestModel>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestModel.Foo), x.Name),
				x => Assert.Equal(nameof(TestModel.Bar), x.Name)
			);
		}

		public sealed record TestModel
		{
			public int Foo { get; set; }

			public string Bar { get; set; } = string.Empty;

			[Ignore]
			public bool Ignore { get; set; }
		}
	}
}
