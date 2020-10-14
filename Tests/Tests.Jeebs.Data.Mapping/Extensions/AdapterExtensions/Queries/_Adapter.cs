using System;
using System.Collections.Generic;
using System.Text;
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
		public static string __(object o)
			=> $"[{o}]";

		/// <summary>
		/// Get configured adapter
		/// </summary>
		public static IAdapter GetAdapter()
		{
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => __(x.Arg<string>()));
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => __(x.ArgAt<string>(0)));
			return adapter;
		}
	}
}
