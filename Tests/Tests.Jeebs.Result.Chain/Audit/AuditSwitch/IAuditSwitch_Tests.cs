namespace Jeebs.AuditTests
{
	public interface IAuditSwitch_Tests
	{
		void IError_Input_Catches_Exception();
		void IError_Input_When_IError_Runs_Func();
		void IError_Input_When_IOkV_Does_Nothing();
		void IError_Input_When_IOk_Does_Nothing();
		void IOkV_Input_Catches_Exception();
		void IOkV_Input_When_IError_Does_Nothing();
		void IOkV_Input_When_IOkV_Runs_Func();
		void IOkV_Input_When_IOk_Does_Nothing();
		void IOk_Input_Catches_Exception();
		void IOk_Input_When_IError_Does_Nothing();
		void IOk_Input_When_IOkV_Does_Nothing();
		void IOk_Input_When_IOk_Runs_Func();
		void No_Input_Returns_Original_Result();
		void Unknown_Implementation_Throws_Exception();
	}
}