using NSubstitute;

namespace Jeebs.Data.Adapter_Tests
{
	public static class Adapter
	{
		public const char SchemaSeparator = '.';
		public const char ColumnSeparator = '|';
		public const char ListSeparator = '+';
		public const char EscapeOpen = '[';
		public const char EscapeClose = ']';
		public const string Alias = "AS";
		public const char AliasOpen = '{';
		public const char AliasClose = '}';
		public const string SortAsc = "ASC";
		public const string SortDesc = "DESC";

		public static Data.Adapter GetAdapter()
			=> Substitute.ForPartsOf<Data.Adapter>(SchemaSeparator, ColumnSeparator, ListSeparator, EscapeOpen, EscapeClose, Alias, AliasOpen, AliasClose, SortAsc, SortDesc);
	}
}
