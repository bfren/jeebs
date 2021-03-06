using System.Threading.Tasks;

namespace AppMvc
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) =>
			await Main<App>(args).ConfigureAwait(false);
	}
}