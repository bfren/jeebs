// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Azure.Identity;
using Jeebs.Config.Azure.KeyVault;

namespace Jeebs.Apps;

public static partial class KeyVaultConfigExtensions
{
	/// <summary>
	/// Create a <see cref="ClientSecretCredential"/> from a <see cref="KeyVaultConfig"/> object.
	/// </summary>
	/// <param name="this"></param>
	public static ClientSecretCredential GetCredential(this KeyVaultConfig @this) =>
		new(@this.TenantId, @this.ClientId, @this.ClientSecret);
}
