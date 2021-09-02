// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.MimeType_Tests
{
	public class AddCustomMimeType_Tests
	{
		[Fact]
		public void Adds_Custom_MimeType_To_HashSet()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new MimeType(name);

			// Act
			var result = MimeType.AddCustomMimeType(type);

			// Assert
			Assert.True(result);
			Assert.Contains(MimeType.AllTest(),
				x => x.Equals(type)
			);
		}

		[Fact]
		public void Does_Not_Add_Custom_MimeType_Twice()
		{
			// Arrange
			var name = F.Rnd.Str;
			var type = new MimeType(name);
			MimeType.AddCustomMimeType(type);

			// Act
			var result = MimeType.AddCustomMimeType(type);

			// Assert
			Assert.False(result);
			Assert.Contains(MimeType.AllTest(),
				x => x.Equals(type)
			);
		}
	}
}
