using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Term List
	/// </summary>
	public sealed class TermList : List<TermList.Term>
	{
		/// <summary>
		/// Taxonomy of this Term List
		/// </summary>
		public Taxonomy Taxonomy { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="taxonomy">Taxonomy of these terms</param>
		public TermList(Taxonomy taxonomy) => Taxonomy = taxonomy;

		/// <summary>
		/// Term Model
		/// </summary>
		public class Term
		{
			/// <summary>
			/// TermId
			/// </summary>
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
			/// Count
			/// </summary>
			public long Count { get; set; }

			/// <summary>
			/// Display title
			/// </summary>
			public override string ToString() => Title;
		}
	}
}
