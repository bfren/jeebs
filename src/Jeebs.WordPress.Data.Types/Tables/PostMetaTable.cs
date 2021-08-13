﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// Post Meta Table
	/// </summary>
	public sealed record class PostMetaTable : Table
	{
		/// <summary>
		/// PostMetaId
		/// </summary>
		public string Id =>
			"meta_id";

		/// <summary>
		/// PostId
		/// </summary>
		public string PostId =>
			"post_id";

		/// <summary>
		/// Key
		/// </summary>
		public string Key =>
			"meta_key";

		/// <summary>
		/// Value
		/// </summary>
		public string Value =>
			"meta_value";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public PostMetaTable(string prefix) : base($"{prefix}postmeta") { }
	}
}
