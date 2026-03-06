// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Auth.Models;

/// <summary>
/// Implementation of basic Auth User model.
/// </summary>
public sealed record class AuthUserModel : Jeebs.Auth.Data.Models.AuthUserModel<AuthRoleModel>;
