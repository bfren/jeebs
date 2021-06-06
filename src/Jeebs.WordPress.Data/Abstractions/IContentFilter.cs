// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Content filter
	/// </summary>
	public interface IContentFilter
	{
		/// <summary>
		/// Execute filter
		/// </summary>
		/// <param name="content">Original content</param>
		string Execute(string content);
	}
}
