﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IMappedColumnList"/>
	public sealed record class MappedColumnList : ImmutableList<IMappedColumn>, IMappedColumnList
	{
		/// <summary>
		/// Create empty list
		/// </summary>
		public MappedColumnList() { }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="collection">IEnumerable</param>
		public MappedColumnList(IEnumerable<IMappedColumn> collection) : base(collection) { }
	}
}
