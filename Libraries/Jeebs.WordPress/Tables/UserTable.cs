using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed class UserTable : Table
	{
		/// <summary>
		/// UserId
		/// </summary>
		public readonly string UserId = "ID";

		/// <summary>
		/// Username
		/// </summary>
		public readonly string Username = "user_login";

		/// <summary>
		/// Password
		/// </summary>
		public readonly string Password = "user_pass";

		/// <summary>
		/// Slug
		/// </summary>
		public readonly string Slug = "user_nicename";

		/// <summary>
		/// Email
		/// </summary>
		public readonly string Email = "user_email";

		/// <summary>
		/// Url
		/// </summary>
		public readonly string Url = "user_url";

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public readonly string RegisteredOn = "user_registered";

		/// <summary>
		/// ActivationKey
		/// </summary>
		public readonly string ActivationKey = "user_activation_key";

		/// <summary>
		/// Status
		/// </summary>
		public readonly string Status = "user_status";

		/// <summary>
		/// DisplayName
		/// </summary>
		public readonly string DisplayName = "display_name";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public UserTable(in string prefix) : base($"{prefix}users") { }
	}
}
