// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using System.Text.RegularExpressions;
using Jeebs.Config.ConfigValidator_Tests;

namespace Jeebs.Config.Resources;

public partial class Schema_Tests
{
	[Fact]
	public void Returns_Byte_Array()
	{
		// Arrange

		// Act
		var result = Properties.Resources.schema;

		// Assert
		Assert.IsType<byte[]>(result);
	}

	[GeneratedRegex("jeebsconfig_1json__JsonSchema=\"([^\"]+)\"")]
	private static partial Regex CsprojSchema();

	[Theory]
	[MemberData(nameof(Validate_Tests.AppConfigs), MemberType = typeof(Validate_Tests))]
	public void Sample_App_Points_To_Schema_Id(string path)
	{
		// Arrange
		using var schema = JsonDocument.Parse(Properties.Resources.schema);
		var id = schema.RootElement.GetProperty("$id").GetString();
		var options = new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true };
		using var config = JsonDocument.Parse(File.ReadAllBytes(path), options);
		var csprojFiles = Directory.GetFiles(Path.GetDirectoryName(path)!, "*.csproj");

		// Act
		var configSchema = config.RootElement.GetProperty("$schema").GetString();
		var csprojSchemas = csprojFiles.Select(x => CsprojSchema().Match(File.ReadAllText(x))).Where(x => x.Success).Select(x => x.Groups[1].Value).ToList();

		// Assert
		Assert.Equal(id, configSchema);
		Assert.NotEmpty(csprojSchemas);
		Assert.All(csprojSchemas, x => Assert.Equal(id, x));
	}
}
