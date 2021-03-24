﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Comment Meta Table
	/// </summary>
	public sealed class CommentMetaTable : Table
	{
		/// <summary>
		/// CommentMetaId
		/// </summary>
		public string CommentMetaId =>
			"meta_id";

		/// <summary>
		/// CommentId
		/// </summary>
		public string CommentId =>
			"comment_id";

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
		/// <param name="prefix">Table Prefix</param>
		public CommentMetaTable(string prefix) : base($"{prefix}commentmeta") { }
	}
}