using System.Threading.Tasks;

namespace Tests.Jeebs.Result.LinkMapAsync
{
	public interface ILinkMapAsync_Task0_NoInput
	{
		Task StartAsync_Successful_Returns_OkWithValue();
		Task StartAsync_Unsuccessful_Adds_Exception_Message();
		Task StartAsync_Unsuccessful_Returns_Error();
		Task StartAsync_Unsuccessful_Then_SkipsAhead();
		Task StartSync_Successful_Returns_OkWithValue();
		Task StartSync_Unsuccessful_Adds_Exception_Message();
		Task StartSync_Unsuccessful_Returns_Error();
		Task StartSync_Unsuccessful_Then_SkipsAhead();
	}
}