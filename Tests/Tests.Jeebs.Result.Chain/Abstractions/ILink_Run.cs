namespace Jeebs.LinkTests
{
	public interface ILink_Run
	{
		void Expecting_IOkV_But_IError_Returns_IError();
		void Expecting_IOk_But_IError_Returns_IError();
		void Expecting_IOk_With_Value_But_IError_Returns_IError();
		void When_IOk_Runs_Action_Without_Input_Catches_Exception_Returns_Error();
		void When_IOk_Runs_Action_Without_Input_Returns_Original_Result();
		void When_IOk_Runs_Action_With_IOkV_Input_Catches_Exception_Returns_Error();
		void When_IOk_Runs_Action_With_IOkV_Input_Returns_Original_Result();
		void When_IOk_Runs_Action_With_IOk_Input_Catches_Exception_Returns_Error();
		void When_IOk_Runs_Action_With_IOk_Input_Returns_Original_Result();
		void When_IOk_Runs_Action_With_IOk_Value_Input_Catches_Exception_Returns_Error();
		void When_IOk_Runs_Action_With_IOk_Value_Input_Returns_Original_Result();
	}
}