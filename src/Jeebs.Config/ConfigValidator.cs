// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
		using var configFile = File.OpenText(path);
		using var configReader = new JsonTextReader(configFile);
		var config = JToken.ReadFrom(configReader);

		// Read schema file
		using var schemaStream = new MemoryStream(Properties.Resources.schema);
		using var schemaFile = new StreamReader(schemaStream);
		using var schemaReader = new JsonTextReader(schemaFile);
		var schema = JSchema.Load(schemaReader);

		// Validate file using schema
		if (!config.IsValid(schema, out IList<string> errors))
		{
			var sb = new StringBuilder();
			_ = sb.AppendLine(CultureInfo.InvariantCulture, $"Invalid Jeebs configuration file: {path}.");
			foreach (var item in errors)
			{
				_ = sb.AppendLine(item);
			}

			throw new ConfigSchemaValidationFailedException(sb.ToString());
		}

		return true;
	}
}
