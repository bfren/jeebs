// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace Jeebs.Config.ConfigurationExtensions_Tests;

public class GetSection_Tests
{
	[Fact]
	public void Calls_GetSection_With_Defined_Section_Key()
	{
		// Arrange
		var config = Substitute.For<IConfiguration>();
		var key = F.Rnd.Str;

		// Act
		config.GetSection<object>(key, false);

		// Assert
		config.Received().GetSection(key);
	}

	[Fact]
	public void Calls_GetSection_With_Jeebs_Section_Key()
	{
		// Arrange
		var config = Substitute.For<IConfiguration>();
		var key = $":{F.Rnd.Str}";

		// Act
		config.GetSection<object>(key, false);

		// Assert
		config.Received().GetSection($"jeebs{key}");
	}

	[Fact]
	public void Uses_Cache_After_First_Call()
	{
		// Arrange
		var config = Substitute.For<IConfiguration>();
		var key = $":{F.Rnd.Str}";

		// Act
		config.GetSection<object>(key);
		config.GetSection<object>(key);
		config.GetSection<object>(key);

		// Assert
		config.Received(1).GetSection($"jeebs{key}");
	}

	[Fact]
	public void Binds_Configuration_Values_To_Object()
	{
		// Arrange		
		var key = F.Rnd.Str;
		var b0 = F.Rnd.Str;
		var b1 = F.Rnd.Int;

		var builder = new ConfigurationBuilder();
		builder.AddInMemoryCollection(new Dictionary<string, string>
		{
			{ $"{key}:{nameof(Foo.Bar0)}", b0 },
			{ $"{key}:{nameof(Foo.Bar1)}", b1.ToString() }
		});

		var config = builder.Build();

		// Act
		var result = config.GetSection<Foo>(key, false);

		// Assert
		Assert.Equal(b0, result.Bar0);
		Assert.Equal(b1, result.Bar1);
	}
}
