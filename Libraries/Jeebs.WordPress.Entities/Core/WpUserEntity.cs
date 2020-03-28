using System;
using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public abstract class WpUserEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => UserId; set => UserId = value; }

		/// <summary>
		/// UserId
		/// </summary>
		[Id]
		public long UserId { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; } = string.Empty;

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; } = string.Empty;

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public DateTime RegisteredOn { get; set; }

		/// <summary>
		/// ActivationKey
		/// </summary>
		public string ActivationKey { get; set; } = string.Empty;

		/// <summary>
		/// Status
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName { get; set; } = string.Empty;
	}
}
