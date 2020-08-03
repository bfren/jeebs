namespace Jeebs.LinkTests
{
	public interface ILink_Map
	{
		void Expecting_IOkV_But_IError_Returns_IError();
		void Expecting_IOk_But_IError_Returns_IError();
		void When_IOkV_Catches_Exception_Returns_IError();
		void When_IOkV_Runs_Func();
		void When_IOk_Catches_Exception_Returns_IError();
		void When_IOk_Runs_Func();
	}
}