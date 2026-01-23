// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Services;

/// <summary>
/// Third-party services configuration.
/// </summary>
public sealed record class ServicesConfig : IOptions<ServicesConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":services";

	/// <summary>
	/// Console configurations.
	/// </summary>
	public Dictionary<string, Console.ConsoleConfig> Console { get; init; } = [];

	/// <summary>
	/// Seq configurations.
	/// </summary>
	public Dictionary<string, Seq.SeqConfig> Seq { get; init; } = [];

	/// <summary>
	/// Slack configurations.
	/// </summary>
	public Dictionary<string, Slack.SlackConfig> Slack { get; init; } = [];

	/// <inheritdoc/>
	ServicesConfig IOptions<ServicesConfig>.Value =>
		this;

	/// <summary>
	/// Get a service configuration from the definition.
	/// 
	/// </summary>
	/// <remarks>
	/// Definition must be in the format <c>service_type.service_name</c>
	/// </remarks>
	/// <param name="definition">Service definition - in format <c>service_type.service_name</c></param>
	/// <returns>Service configuration.</returns>
	public Result<IServiceConfig> GetServiceConfig(string definition) =>
		SplitDefinition(definition).Match(
			none: () => R.Fail("Invalid service definition: {Definition}.", new { definition })
				.Ctx(nameof(ServicesConfig), nameof(GetServiceConfig)),
			some: x => x switch
			{
				("console", string name) =>
				   GetServiceConfig(c => c.Console, name).Map(c => (IServiceConfig)c),

				("seq", string name) =>
					GetServiceConfig(c => c.Seq, name).Map(c => (IServiceConfig)c),

				("slack", string name) =>
					GetServiceConfig(c => c.Slack, name).Map(c => (IServiceConfig)c),

				(string type, var _) =>
					R.Fail("Service type '{Type}' is not recognised.", new { type })
						.Ctx(nameof(ServicesConfig), nameof(GetServiceConfig))
			}
		);

	/// <summary>
	/// Get a named service configuration.
	/// </summary>
	/// <typeparam name="T">Service Config type</typeparam>
	/// <param name="getCollection">The service collection to use.</param>
	/// <param name="name">The name of the service to get.</param>
	/// <returns>Service configuration.</returns>
	public Result<T> GetServiceConfig<T>(Func<ServicesConfig, Dictionary<string, T>> getCollection, string name)
		where T : IServiceConfig, new() =>
		getCollection(this).GetValueOrNone(name).Match(
			none: () => new T(),
			some: x => x.IsValid switch
			{
				true =>
					R.Wrap(x),

				false =>
					R.Fail("Definition of {Type} service named '{Name}' is invalid.", new { Type = typeof(T).Name, name })
						.Ctx(nameof(ServicesConfig), nameof(GetServiceConfig))
			}
		);

	/// <summary>
	/// Split a service definition.
	/// </summary>
	/// <param name="definition">Service definition - in format <c>service_type.service_name</c>.</param>
	/// <returns>Definition tuple.</returns>
	public static Maybe<(string type, string name)> SplitDefinition(string? definition) =>
		string.IsNullOrWhiteSpace(definition) switch
		{
			false =>
				definition.Split('.') switch
				{
					var x when x.Length == 1 =>
						(x[0], string.Empty),

					var x when x.Length == 2 =>
						(x[0], x[1]),

					_ =>
						M.None
				},

			true =>
				M.None
		};
}
