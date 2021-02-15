using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract record WpTermMetaEntity : IEntity, IEntity<long>
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
				new WpTermMetaId(TermMetaId);

			init =>
				TermMetaId = value.Value;
		}

		/// <summary>
		/// TermMetaId
		/// </summary>
		[Id]
		public long TermMetaId { get; set; }

		/// <summary>
		/// TermId
		/// </summary>
		public long TermId { get; set; }

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
