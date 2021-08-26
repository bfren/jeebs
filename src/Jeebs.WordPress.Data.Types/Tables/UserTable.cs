// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed record class UserTable : Table
	{
		/// <summary>
		/// UserId
		/// </summary>
		public string Id =>
			"ID";

		/// <summary>
		/// Username
		/// </summary>
		public string Username =>
			"user_login";

		/// <summary>
		/// Password
		/// </summary>
		public string Password =>
			"user_pass";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug =>
			"user_nicename";

		/// <summary>
		/// Email
		/// </summary>
		public string Email =>
			"user_email";

		/// <summary>
		/// Url
		/// </summary>
		public string Url =>
			"user_url";

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public string RegisteredOn =>
			"user_registered";

		/// <summary>
		/// ActivationKey
		/// </summary>
		public string ActivationKey =>
			"user_activation_key";

		/// <summary>
		/// Status
		/// </summary>
		public string Status =>
			"user_status";

		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName =>
			"display_name";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public UserTable(string prefix) : base($"{prefix}users") { }
	}
}
