// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.MimeType_Tests;

public class AddCustomMimeType_Tests
{
	[Fact]
	public void Adds_Custom_MimeType_To_HashSet()
	{
		// Arrange
		var name = Rnd.Str;
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
		var name = Rnd.Str;
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

	[Fact]
	public void Adding_While_Parsing_On_Other_Threads__Adds_Every_Type_Without_Error()
	{
		// Arrange
		var types = Enumerable.Range(0, 2000).Select(_ => new MimeType(Rnd.Str)).ToList();

		// Act - parse on every other iteration, so reads and writes overlap
		Parallel.For(0, types.Count, i =>
		{
			_ = MimeType.AddCustomMimeType(types[i]);
			_ = MimeType.Parse(i % 2 == 0 ? MimeType.Pdf.ToString() : Rnd.Str);
		});

		// Assert
		var all = MimeType.AllTest();
		Assert.All(types, t => Assert.Contains(t, all));
	}
}
