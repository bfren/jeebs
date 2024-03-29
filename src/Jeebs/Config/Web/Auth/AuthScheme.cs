// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Web.Auth;

/// <summary>
/// Supported authentication schemes
/// </summary>
public enum AuthScheme
{
	/// <summary>
	/// Cookies authentication
	/// </summary>
	Cookies = 1 << 0,

	/// <summary>
	/// JWT authentication
	/// </summary>
	Jwt = 1 << 1
}
