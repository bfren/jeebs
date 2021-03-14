// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace AppRazor
{
	public sealed class Program : Jeebs.Apps.Program
	{
		public static async Task Main(string[] args) =>
			await MainAsync<App>(args).ConfigureAwait(false);
	}
}
