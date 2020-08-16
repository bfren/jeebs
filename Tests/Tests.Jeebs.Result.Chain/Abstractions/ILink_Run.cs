namespace Jeebs.Link_Tests
{
	public interface ILink_Run
	{
		void IOk_Input_When_IError_Returns_IError();
		void IOk_Input_When_IOk_Catches_Exception();
		void IOk_Input_When_IOk_Runs_Action();
		void IOk_ValueType_Input_When_IError_Returns_IError();
		void IOk_ValueType_Input_When_IOk_Catches_Exception();
		void IOk_ValueType_Input_When_IOk_Runs_Action();
		void IOk_Value_Input_When_IError_Returns_IError();
		void IOk_Value_Input_When_IOk_Catches_Exception();
		void IOk_Value_Input_When_IOk_Runs_Action();
		void No_Input_When_IError_Returns_IError();
		void No_Input_When_IOk_Catches_Exception();
		void No_Input_When_IOk_Runs_Action();
	}
}