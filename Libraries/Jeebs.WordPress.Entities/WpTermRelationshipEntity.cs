using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract class WpTermRelationshipEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id
		{
			get => PostId;
			set => PostId = value;
		}

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; set; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public long TermTaxonomyId { get; set; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public long SortOrder { get; set; }
	}
}
