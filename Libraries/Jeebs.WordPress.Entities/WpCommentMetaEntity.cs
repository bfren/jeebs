using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// CommentMeta entity
	/// </summary>
	public abstract record WpCommentMetaEntity : IEntity, IEntity<long>
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
				new WpCommentMetaId(CommentMetaId);

			init =>
				CommentMetaId = value.Value;
		}

		/// <summary>
		/// CommentMetaId
		/// </summary>
		[Id]
		public long CommentMetaId { get; set; }

		/// <summary>
		/// CommentId
		/// </summary>
		public long CommentId { get; set; }

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
