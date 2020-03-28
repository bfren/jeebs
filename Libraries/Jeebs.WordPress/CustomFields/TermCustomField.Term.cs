using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Term Taxonomy Custom Field
	/// </summary>
	public abstract partial class TermCustomField
	{
		/// <summary>
		/// Term class
		/// </summary>
		public sealed class Term : IEntity
		{
			/// <summary>
			/// Id
			/// </summary>
			public long Id { get => TermId; set => TermId = value; }

			/// <summary>
			/// TermId
			/// </summary>
			public long TermId { get; set; }

			/// <summary>
			/// Taxonomy
			/// </summary>
			public string Taxonomy { get; set; } = string.Empty;

			/// <summary>
			/// Description
			/// </summary>
			public string Description { get; set; } = string.Empty;

			/// <summary>
			/// Title
			/// </summary>
			public string Title { get; set; } = string.Empty;

			/// <summary>
			/// Slug
			/// </summary>
			public string Slug { get; set; } = string.Empty;

			/// <summary>
			/// Count
			/// </summary>
			public int Count { get; set; }
		}
	}
}
