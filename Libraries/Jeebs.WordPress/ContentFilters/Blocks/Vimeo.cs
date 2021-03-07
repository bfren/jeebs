// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.ContentFilters.Blocks
{
	/// <summary>
	/// Vimeo block
	/// </summary>
	internal sealed class Vimeo : Embed
	{
		/// <summary>
		/// Output format
		/// </summary>
		private const string format = @"<div id=""{0}"" class=""hide video-vimeo"" data-url=""{1}""></div>";

		public Vimeo() : base(EmbedType.Video, Provider.Vimeo) { }

		protected override string Format(string id, Parsed block) =>
			string.Format(format, id, block.Url);
	}
}
