using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;

namespace Jeebs.Data.Adapter_Tests
{
	public static class Adapter
	{
		public const char SchemaSeparator = '.';
		public const string ColumnSeparator = "|";
		public const char EscapeOpen = '[';
		public const char EscapeClose = ']';
		public const string Alias = "AS";
		public const char AliasOpen = '{';
		public const char AliasClose = '}';
		public const string SortAsc = "ASC";
		public const string SortDesc = "DESC";

		public static Data.Adapter GetAdapter()
			=> Substitute.For<Data.Adapter>(SchemaSeparator, ColumnSeparator, EscapeOpen, EscapeClose, Alias, AliasOpen, AliasClose, SortAsc, SortDesc);
	}
}
