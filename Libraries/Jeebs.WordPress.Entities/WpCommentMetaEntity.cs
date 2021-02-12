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
	public abstract record WpCommentMetaEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id
		{
			get =>
				CommentMetaId;

			set =>
				CommentMetaId = value;
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
