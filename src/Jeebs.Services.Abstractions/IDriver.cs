// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config;

namespace Jeebs.Services;

/// <summary>
/// Service driver
/// </summary>
/// <typeparam name="TConfig">Service configuration type</typeparam>
public interface IDriver<TConfig> where TConfig : IServiceConfig { }
