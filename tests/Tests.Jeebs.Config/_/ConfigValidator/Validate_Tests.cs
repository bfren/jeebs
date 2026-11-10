// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.ConfigValidator_Tests;

public class Validate_Tests
{
	private static async Task<bool> ValidateAsync(string json)
	{
		var path = Path.GetTempFileName();
		try
		{
			await File.WriteAllTextAsync(path, json, TestContext.Current.CancellationToken);
			return ConfigValidator.Validate(path);
		}
		finally
		{
			File.Delete(path);
		}
	}

	[Fact]
	public void Missing_File__Throws_FileNotFoundException()
	{
		// Arrange
		var path = Path.Combine(Path.GetTempPath(), Rnd.Str + ".json");

		// Act
		var result = Record.Exception(() => ConfigValidator.Validate(path));

		// Assert
		Assert.IsType<FileNotFoundException>(result);
	}

	[Fact]
	public async Task Valid_Config__Returns_True()
	{
		// Arrange
		const string json = /*lang=json,strict*/ """{ "jeebs": { "app": { "name": "Test" } } }""";

		// Act
		var result = await ValidateAsync(json);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public async Task Comments_And_Trailing_Commas__Returns_True()
	{
		// Arrange
		const string json = /*lang=json*/ """
			{
				// comment
				"jeebs": { "app": { "name": "Test", }, },
			}
			""";

		// Act
		var result = await ValidateAsync(json);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(/*lang=json,strict*/ """{ "jeebs": { } }""")]
	[InlineData(/*lang=json,strict*/ """{ "jeebs": { "app": { "name": "Test" }, "unknown": true } }""")]
	[InlineData(/*lang=json,strict*/ """{ "jeebs": { "app": { "name": "Test" }, "logging": { "minimum": "Loud" } } }""")]
	public async Task Invalid_Config__Throws_ConfigSchemaValidationFailedException(string json)
	{
		// Arrange

		// Act
		var result = await Record.ExceptionAsync(() => ValidateAsync(json));

		// Assert
		var ex = Assert.IsType<ConfigSchemaValidationFailedException>(result);
		Assert.StartsWith("Invalid Jeebs configuration file:", ex.Message);
	}

	public static TheoryData<string> AppConfigs()
	{
		var dir = new DirectoryInfo(AppContext.BaseDirectory);
		while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "Jeebs (v10).slnx")))
		{
			dir = dir.Parent;
		}

		var sep = Path.DirectorySeparatorChar;
		return [..
			Directory.GetFiles(Path.Combine(dir!.FullName, "apps"), "jeebsconfig.json", SearchOption.AllDirectories)
				.Where(x => !x.Contains($"{sep}bin{sep}") && !x.Contains($"{sep}obj{sep}"))
		];
	}

	[Theory]
	[MemberData(nameof(AppConfigs))]
	public void Sample_App_Config__Returns_True(string path)
	{
		// Arrange

		// Act
		var result = ConfigValidator.Validate(path);

		// Assert
		Assert.True(result);
	}
}
