using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

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
		public string UserId { get; } = "ID";

		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; } = "user_login";

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; } = "user_pass";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; } = "user_nicename";

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; } = "user_email";

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; } = "user_url";

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public string RegisteredOn { get; } = "user_registered";

		/// <summary>
		/// ActivationKey
		/// </summary>
		public string ActivationKey { get; } = "user_activation_key";

		/// <summary>
		/// Status
		/// </summary>
		public string Status { get; } = "user_status";

		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName { get; } = "display_name";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public UserTable(string prefix) : base($"{prefix}users") { }
	}
}
