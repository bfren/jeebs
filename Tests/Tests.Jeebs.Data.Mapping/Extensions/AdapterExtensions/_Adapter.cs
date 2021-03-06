using NSubstitute;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public static class Adapter
	{
		/// <summary>
		/// Shorthand for escaping a string
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
#pragma warning disable IDE1006 // Naming Styles
		public static string __(object o)
#pragma warning restore IDE1006 // Naming Styles
			=> $"[{o}]";

		/// <summary>
		/// Get configured adapter
		/// </summary>
		public static IAdapter GetAdapter()
		{
			var adapter = Substitute.For<IAdapter>();
			adapter.ColumnSeparator
				.Returns('|');
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => __(x.Arg<string>()));
			adapter.Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => __(x.ArgAt<string>(0)));
			return adapter;
		}
	}
}
