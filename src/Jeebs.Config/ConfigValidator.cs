// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Jeebs.Config.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Jeebs.Config;

/// <summary>
/// Jeebs configuration validator
/// </summary>
public static class ConfigValidator
{
	/// <summary>
	/// Validate a Jeebs config file against the schema
	/// </summary>
	/// <exception cref="FileNotFoundException"></exception>
	/// <exception cref="ConfigurationSchemaValidationFailedException"></exception>
	/// <param name="path">Absolute path to Jeebs configuration file</param>
	public static string Validate(string path)
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
		using var schemaStream = new MemoryStream(Resources.schema);
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

			throw new ConfigurationSchemaValidationFailedException(sb.ToString());
		}

		// Return original path on success
		return path;
	}
}
