using System.Threading.Tasks;

namespace Tests.Jeebs.Result_old.LinkAsync
{
	public interface ILinkAsync_Task2_InputWithValue
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