// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace AppWeb
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) =>
			await Main<App>(args).ConfigureAwait(false);
	}
}
