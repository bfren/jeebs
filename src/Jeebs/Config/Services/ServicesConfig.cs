// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Services;

/// <summary>
/// Third-party services configuration
/// </summary>
public sealed record class ServicesConfig : IOptions<ServicesConfig>
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":services";

	/// <summary>
	/// Console configurations
	/// </summary>
	public Dictionary<string, Console.ConsoleConfig> Console { get; init; } = new();

	/// <summary>
	/// Seq configurations
	/// </summary>
	public Dictionary<string, Seq.SeqConfig> Seq { get; init; } = new();

	/// <summary>
	/// Slack configurations
	/// </summary>
	public Dictionary<string, Slack.SlackConfig> Slack { get; init; } = new();

	/// <inheritdoc/>
	ServicesConfig IOptions<ServicesConfig>.Value =>
		this;

	/// <summary>
	/// Get a service configuration from the definition
	/// <para>Definition must be in the format <c>service_type.service_name</c></para>
	/// </summary>
	/// <exception cref="UnsupportedServiceException"></exception>
	/// <param name="definition">Service definition - in format <c>service_type.service_name</c></param>
	public IServiceConfig GetServiceConfig(string definition) =>
		SplitDefinition(definition) switch
		{
			("console", string name) =>
				GetServiceConfig(c => c.Console, name),

			("seq", string name) =>
				GetServiceConfig(c => c.Seq, name),

			("slack", string name) =>
				GetServiceConfig(c => c.Slack, name),

			(string type, _) =>
				throw new UnsupportedServiceException(type)
		};

	/// <summary>
	/// Get a named service configuration
	/// <para>Checks if it is valid before returning it</para>
	/// </summary>
	/// <typeparam name="TConfig"></typeparam>
	/// <exception cref="UnknownServiceException"></exception>
	/// <exception cref="InvalidServiceConfigurationException"></exception>
	/// <param name="getCollection">The service collection to use</param>
	/// <param name="name">The name of the service to get</param>
	public TConfig GetServiceConfig<TConfig>(Func<ServicesConfig, Dictionary<string, TConfig>> getCollection, string name)
		where TConfig : IServiceConfig, new()
	{
		var services = getCollection(this);
		if (!services.ContainsKey(name))
		{
			return new TConfig();
		}

		var config = services[name];
		if (!config.IsValid)
		{
			throw new InvalidServiceConfigurationException(name, typeof(TConfig));
		}

		return config;
	}

	/// <summary>
	/// Split a service definition
	/// <para>Definition must be in the format <c>service_type.service_name</c></para>
	/// </summary>
	/// <exception cref="InvalidServiceDefinitionException"></exception>
	/// <param name="definition">Service definition - in format <c>service_type.service_name</c></param>
	public static (string type, string name) SplitDefinition(string definition)
	{
		if (string.IsNullOrWhiteSpace(definition))
		{
			throw new InvalidServiceDefinitionException(definition);
		}

		try
		{
			return definition.Split('.') switch
			{
				var x when x.Length == 1 =>
					(x[0], string.Empty),

				var x when x.Length == 2 =>
					(x[0], x[1]),

				_ =>
					throw new InvalidServiceDefinitionException(definition)
			};
		}
		catch (Exception)
		{
			throw new InvalidServiceDefinitionException(definition);
		}
	}
}
