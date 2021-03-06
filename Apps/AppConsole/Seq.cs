using Jeebs.Services.Drivers.Webhook.Seq;

namespace AppConsole
{
	internal class Seq : SeqWebhookDriver
	{
		public Seq(SeqWebhookDriverArgs args) : base("wrong", args) { }
	}
}
