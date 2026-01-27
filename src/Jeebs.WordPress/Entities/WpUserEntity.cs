// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// User entity.
/// </summary>
public abstract record class WpUserEntity : WpUserEntityWithId
{
	/// <summary>
	/// Username.
	/// </summary>
	public string Username { get; init; } = string.Empty;

	/// <summary>
	/// Password.
	/// </summary>
	public string Password { get; init; } = string.Empty;

	/// <summary>
	/// Slug.
	/// </summary>
	public string Slug { get; init; } = string.Empty;

	/// <summary>
	/// Email.
	/// </summary>
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// Url.
	/// </summary>
	public string Url { get; init; } = string.Empty;

	/// <summary>
	/// RegisteredOn.
	/// </summary>
	public DateTime RegisteredOn { get; init; }

	/// <summary>
	/// ActivationKey.
	/// </summary>
	public string ActivationKey { get; init; } = string.Empty;

	/// <summary>
	/// Status.
	/// </summary>
	public uint Status { get; init; }

	/// <summary>
	/// DisplayName.
	/// </summary>
	public string DisplayName { get; init; } = string.Empty;
}
