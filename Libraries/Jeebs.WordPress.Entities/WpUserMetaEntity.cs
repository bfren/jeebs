﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public abstract record WpUserMetaEntity : IEntity, IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		long IEntity.Id =>
			Id.Value;

		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public IStrongId<long> Id
		{
			get =>
				new WpUserMetaId(UserMetaId);

			init =>
				UserMetaId = value.Value;
		}

		/// <summary>
		/// UserMetaId
		/// </summary>
		[Id]
		public long UserMetaId { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; } = string.Empty;
	}
}
