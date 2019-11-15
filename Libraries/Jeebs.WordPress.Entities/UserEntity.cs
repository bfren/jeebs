using System;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public class WpUserEntity
	{
		/// <summary>
		/// UserId
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public DateTime RegisteredOn { get; set; }

		/// <summary>
		/// ActivationKey
		/// </summary>
		public string ActivationKey { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public WpUserEntity()
		{
			Username = string.Empty;
			Password = string.Empty;
			Slug = string.Empty;
			Email = string.Empty;
			Url = string.Empty;
			ActivationKey = string.Empty;
			DisplayName = string.Empty;
		}
	}
}
