using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Term entity
	/// </summary>
	public abstract record WpTermEntity : IEntity, IEntity<long>
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
				new WpTermId(TermId);

			init =>
				TermId = value.Value;
		}

		/// <summary>
		/// TermId
		/// </summary>
		[Id]
		public long TermId { get; set; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; } = string.Empty;

		/// <summary>
		/// Group
		/// </summary>
		public long Group { get; set; }
	}
}
