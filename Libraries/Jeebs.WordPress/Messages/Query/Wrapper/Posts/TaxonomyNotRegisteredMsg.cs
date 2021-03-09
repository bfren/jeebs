// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Jeebs.WordPress;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// See <see cref="QueryWrapper.AddTaxonomiesAsync{TList, TModel}(TList)"/>
	/// </summary>
	public sealed class TaxonomyNotRegisteredMsg : IMsg
	{
		private readonly Jeebs.WordPress.Enums.Taxonomy taxonomy;

		/// <summary>
		/// Save taxonomy
		/// </summary>
		/// <param name="taxonomy">Taxonomy</param>
		public TaxonomyNotRegisteredMsg(Jeebs.WordPress.Enums.Taxonomy taxonomy) =>
			this.taxonomy = taxonomy;

		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString() =>
			$"Taxonomy '{taxonomy}' must be registered in {nameof(IWp.RegisterCustomTaxonomies)}.";
	}
}
