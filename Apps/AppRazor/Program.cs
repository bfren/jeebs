using System.Threading.Tasks;

namespace AppRazor
{
	public sealed class Program : Jeebs.Apps.Program
	{
		public static async Task Main(string[] args) =>
			await Main<App>(args).ConfigureAwait(false);
	}
}
