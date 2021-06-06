// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Entities.Additional
{
	/// <summary>
	/// Attached file
	/// </summary>
	public abstract record WpAttachedFile
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; init; } = string.Empty;

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; init; } = string.Empty;

		/// <summary>
		/// Image URL
		/// </summary>
		public string Url { get; init; } = string.Empty;
	}
}
