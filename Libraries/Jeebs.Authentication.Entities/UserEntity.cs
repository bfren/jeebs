using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Authentication.Entities
{
	/// <summary>
	/// User Entity
	/// </summary>
	public record UserEntity : IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		public IStrongId<long> Id
		{
			get =>
				UserId;

			init =>
				UserId = new UserEntityId(value.Value);
		}

		/// <summary>
		/// User ID
		/// </summary>
		public UserEntityId UserId { get; init; } = new UserEntityId();

		/// <summary>
		/// The user's encrypted password
		/// </summary>
		public string PasswordHash { get; init; } = string.Empty;

		/// <summary>
		/// Given (Christian / first) name
		/// </summary>
		public string GivenName { get; init; } = string.Empty;

		/// <summary>
		/// Family name (surname)
		/// </summary>
		public string FamilyName { get; init; } = string.Empty;

		/// <summary>
		/// Full name - normally GivenName + FamilyName
		/// </summary>
		public string FullName =>
			GetFullName();

		/// <summary>
		/// Function to get the full name of the user
		/// </summary>
		private Func<string> GetFullName { get; init; }

		/// <summary>
		/// Email address
		/// </summary>
		public string EmailAddress { get; init; } = string.Empty;

		/// <summary>
		/// The last time the user signed in
		/// </summary>
		public DateTime? LastSignedIn { get; init; }

		/// <summary>
		/// Whether or not the user account is enabled
		/// </summary>
		public bool IsEnabled { get; init; }

		/// <summary>
		/// Whether or not the user account has super permissions
		/// </summary>
		public bool IsSuper { get; init; }

		/// <summary>
		/// Use the default method of getting the user's full name
		/// </summary>
		protected UserEntity() : this(getFullName: null) { }

		/// <summary>
		/// Inject custom function to get the user's full name
		/// </summary>
		/// <param name="getFullName">[Optional] Function to return the user's full name</param>
		protected UserEntity(Func<string>? getFullName) =>
			GetFullName = getFullName switch
			{
				Func<string> get =>
					get,

				_ =>
					() => string.Format("{0} {1}", GivenName, FamilyName)
			};
	}
}
