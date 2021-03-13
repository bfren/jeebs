// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User with a <see cref="Data.UserId"/> property
	/// </summary>
	public interface IUserWithUserId
	{
		/// <summary>
		/// User ID
		/// </summary>
		UserId UserId { get; init; }
	}
}
