using System.Threading.Tasks;

namespace Jeebs_old.LinkMapAsync
{
	public interface ILinkMapAsync_Task1_InputWithoutValue
	{
		Task StartAsync_Successful_Returns_Ok();
		Task StartAsync_Unsuccessful_Adds_Exception_Message();
		Task StartAsync_Unsuccessful_Returns_Error();
		Task StartAsync_Unsuccessful_Then_SkipsAhead();
		Task StartSync_Successful_Returns_Ok();
		Task StartSync_Unsuccessful_Adds_Exception_Message();
		Task StartSync_Unsuccessful_Returns_Error();
		Task StartSync_Unsuccessful_Then_SkipsAhead();
	}
}