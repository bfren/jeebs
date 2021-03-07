// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Auth.Data.Entities
{
	/// <inheritdoc cref="IUser"/>
	internal sealed record UserEntity : IUser
	{
		/// <inheritdoc/>
		public IStrongId<long> Id
		{
			get =>
				UserId;

			init =>
				UserId = new UserId(value.Value);
		}

		/// <inheritdoc/>
		public string IdStr =>
			Id.ValueStr;

		/// <inheritdoc/>
		public UserId UserId { get; init; } = new UserId();

		/// <inheritdoc/>
		public string EmailAddress { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string PasswordHash { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string FriendlyName { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string GivenName { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string FamilyName { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string FullName =>
			GetFullName();

		/// <inheritdoc/>
		private Func<string> GetFullName { get; init; }

		/// <inheritdoc/>
		public bool IsEnabled { get; init; }

		/// <inheritdoc/>
		public bool IsSuper { get; init; }

		/// <inheritdoc/>
		public DateTime? LastSignedIn { get; init; }

		/// <summary>
		/// Use the default method of getting the user's full name
		/// </summary>
		internal UserEntity() : this(getFullName: null) { }

		/// <summary>
		/// Inject custom function to get the user's full name
		/// </summary>
		/// <param name="getFullName">[Optional] Function to return the user's full name</param>
		internal UserEntity(Func<string>? getFullName) =>
			GetFullName = getFullName switch
			{
				Func<string> get =>
					get,

				_ =>
					() => string.Format("{0} {1}", GivenName, FamilyName)
			};
	}
}
