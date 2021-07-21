﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Enums
{
	/// <summary>
	/// PostStatus enumeration
	/// </summary>
	public sealed class PostStatus : Enumerated
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public PostStatus(string name) : base(name) { }

		#region Default Post Statuses

		/// <summary>
		/// Published post
		/// </summary>
		public static readonly PostStatus Publish = new("publish");

		/// <summary>
		/// Inherit
		/// </summary>
		public static readonly PostStatus Inherit = new("inherit");

		/// <summary>
		/// Pending
		/// </summary>
		public static readonly PostStatus Pending = new("pending");

		/// <summary>
		/// Draft
		/// </summary>
		public static readonly PostStatus Draft = new("draft");

		/// <summary>
		/// Auto Draft
		/// </summary>
		public static readonly PostStatus AutoDraft = new("auto-draft");

		#endregion

		/// <summary>
		/// Parse PostStatus value name
		/// </summary>
		/// <param name="name">Value name</param>
		/// <returns>PostStatus object</returns>
		public static PostStatus Parse(string name) =>
			Parse(name, values: new[] { Publish, Inherit, Pending, Draft, AutoDraft }).Unwrap(() => Draft);
	}
}
