// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.IO;
using System.Text;
using NJsonSchema;

namespace Jeebs.Config;

/// <summary>
/// Jeebs configuration validator.
/// </summary>
public static class ConfigValidator
{
	/// <summary>
	/// Validate a Jeebs config file against the schema.
	/// </summary>
	/// <param name="path">Absolute path to Jeebs configuration file.</param>
	/// <returns>Whether or not the config file is valid.</returns>
	/// <exception cref="FileNotFoundException"/>
	/// <exception cref="ConfigSchemaValidationFailedException"/>
	public static bool Validate(string path)
	{
		// Make sure file exists
		if (!File.Exists(path))
		{
			throw new FileNotFoundException("Jeebs configuration file not found.", path);
		}

		// Read config file
		var config = File.ReadAllText(path);

		// Read schema file - NJsonSchema only loads schemas asynchronously, and this runs once at startup
		using var schemaFile = new StreamReader(new MemoryStream(Properties.Resources.schema));
		var schema = JsonSchema.FromJsonAsync(schemaFile.ReadToEnd()).GetAwaiter().GetResult();

		// Validate file using schema
		var errors = schema.Validate(config);
		if (errors.Count > 0)
		{
			var sb = new StringBuilder();
			_ = sb.AppendLine(F.DefaultCulture, $"Invalid Jeebs configuration file: {path}.");
			foreach (var item in errors)
			{
				_ = sb.AppendLine(item.ToString());
			}

			throw new ConfigSchemaValidationFailedException(sb.ToString());
		}

		return true;
	}
}
