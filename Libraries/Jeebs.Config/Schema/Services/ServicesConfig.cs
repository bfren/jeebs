﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Third-party services configuration
	/// </summary>
	public class ServicesConfig
	{
		/// <summary>
		/// Rocket.Chat configurations
		/// </summary>
		public Dictionary<string, RocketChatConfig> RocketChat { get; set; } = new Dictionary<string, RocketChatConfig>();

		/// <summary>
		/// Seq configurations
		/// </summary>
		public Dictionary<string, SeqConfig> Seq { get; set; } = new Dictionary<string, SeqConfig>();

		/// <summary>
		/// Slack configurations
		/// </summary>
		public Dictionary<string, SlackConfig> Slack { get; set; } = new Dictionary<string, SlackConfig>();

		/// <summary>
		/// Twitter configurations
		/// </summary>
		public Dictionary<string, TwitterConfig> Twitter { get; set; } = new Dictionary<string, TwitterConfig>();

		/// <summary>
		/// Get a service configuration from the definition
		/// <para>Definition must be in the format <c>service_type.service_name</c></para>
		/// </summary>
		/// <exception cref="Jx.Config.UnsupportedServiceException"></exception>
		/// <param name="definition">Service definition - in format <c>service_type.service_name</c></param>
		public ServiceConfig GetServiceConfig(string definition)
			=> SplitDefinition(definition) switch
			{
				("rocketChat", string name) =>
					GetServiceConfig(c => c.RocketChat, name),

				("seq", string name) =>
					GetServiceConfig(c => c.Seq, name),

				("slack", string name) =>
					GetServiceConfig(c => c.Slack, name),

				("twitter", string name) =>
					GetServiceConfig(c => c.Twitter, name),

				(string type, _) =>
					throw new Jx.Config.UnsupportedServiceException(type)
			};

		/// <summary>
		/// Get a named service configuration
		/// <para>Checks if it is valid before returning it</para>
		/// </summary>
		/// <typeparam name="TConfig"></typeparam>
		/// <exception cref="Jx.Config.UnknownServiceException"></exception>
		/// <exception cref="Jx.Config.InvalidServiceConfigurationException"></exception>
		/// <param name="getCollection">The service collection to use</param>
		/// <param name="name">The name of the service to get</param>
		public TConfig GetServiceConfig<TConfig>(Func<ServicesConfig, Dictionary<string, TConfig>> getCollection, string name)
			where TConfig : ServiceConfig
		{
			var services = getCollection(this);
			if (!services.ContainsKey(name))
			{
				throw new Jx.Config.UnknownServiceException(name, typeof(TConfig));
			}

			var config = services[name];
			if (!config.IsValid)
			{
				throw new Jx.Config.InvalidServiceConfigurationException(name, typeof(TConfig));
			}

			return config;
		}

		/// <summary>
		/// Split a service definition
		/// <para>Definition must be in the format <c>service_type.service_name</c></para>
		/// </summary>
		/// <exception cref="Jx.Config.InvalidServiceDefinitionException"></exception>
		/// <param name="definition">Service definition - in format <c>service_type.service_name</c></param>
		public static (string type, string name) SplitDefinition(string definition)
		{
			try
			{
				return definition.Split('.') switch
				{
					var x when x.Length == 2 =>
						(x[0], x[1]),

					_ =>
						throw new Jx.Config.InvalidServiceDefinitionException(definition)
				};
			}
			catch (Exception)
			{
				throw new Jx.Config.InvalidServiceDefinitionException(definition);
			}
		}
	}
}
