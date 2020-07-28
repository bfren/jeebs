using System.Collections.Generic;
using System.Linq;

namespace Jeebs.WordPress.Enums
{
	/// <summary>
	/// Taxonomy enumeration
	/// </summary>
	public sealed class Taxonomy : Enum
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public Taxonomy(string name) : base(name) { }

		#region Default Taxonomies

		/// <summary>
		/// Blank
		/// </summary>
		public static readonly Taxonomy Blank = new Taxonomy("");

		/// <summary>
		/// Category
		/// </summary>
		public static readonly Taxonomy PostCategory = new Taxonomy("category");

		/// <summary>
		/// Tag
		/// </summary>
		public static readonly Taxonomy PostTag = new Taxonomy("post_tag");

		/// <summary>
		/// Link
		/// </summary>
		public static readonly Taxonomy LinkCategory = new Taxonomy("link_category");

		/// <summary>
		/// Menu Item
		/// </summary>
		public static readonly Taxonomy NavMenu = new Taxonomy("nav_menu");

		#endregion

		/// <summary>
		/// List of all post types
		/// Must be public static so it is thread safe
		/// </summary>
		private static readonly HashSet<Taxonomy> all;

		/// <summary>
		/// Populate list of post types
		/// </summary>
		static Taxonomy()
			=> all = new HashSet<Taxonomy>(new[] { PostCategory, PostTag, LinkCategory, NavMenu });

		/// <summary>
		/// Add a custom taxonomy
		/// </summary>
		/// <param name="taxonomy">Taxonomy to add</param>
		/// <returns>False if the taxonomy already exists</returns>
		public static bool AddCustomTaxonomy(Taxonomy taxonomy)
			=> all.Add(taxonomy);

		/// <summary>
		/// Parse Taxonomy value name
		/// </summary>
		/// <param name="name">Value name</param>
		/// <returns>Taxonomy object</returns>
		public static Option<Taxonomy> Parse(string name)
			=> Parse(name, all.ToArray());

		/// <summary>
		/// Returns whether or not the specified taxonomy has been registered
		/// </summary>
		/// <param name="taxonomy">Taxonomy to check</param>
		public static bool IsRegistered(Taxonomy taxonomy)
			=> IsRegistered(taxonomy.ToString(), all.ToArray());
	}
}
