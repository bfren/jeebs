using System.Threading.Tasks;

namespace Jeebs_old.LinkMapAsync
{
	public interface ILinkMapAsync_Task2_InputWithValue
	{
		Task StartAsync_Successful_Returns_OkV();
		Task StartAsync_Unsuccessful_Adds_Exception_Message();
		Task StartAsync_Unsuccessful_Returns_Error();
		Task StartAsync_Unsuccessful_Then_SkipsAhead();
		Task StartSync_Successful_Returns_OkV();
		Task StartSync_Unsuccessful_Adds_Exception_Message();
		Task StartSync_Unsuccessful_Returns_Error();
		Task StartSync_Unsuccessful_Then_SkipsAhead();
	}
}