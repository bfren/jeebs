namespace Jeebs.LinkTests
{
	public interface ILink_Handle
	{
		void Generic_AsyncHandler_Returns_Original_Link();
		void Generic_AsyncHandler_Runs_For_Any_Exception();
		void Generic_Handler_Returns_Original_Link();
		void Generic_Handler_Runs_For_Any_Exception();
		void No_Handler_With_Generic_Custom_ExceptionMsg_Adds_Msg();
		void No_Handler_With_Specific_Custom_ExceptionMsg_Adds_Msg();
		void Specific_AsyncHandler_Does_Not_Run_For_Other_Exceptions();
		void Specific_AsyncHandler_Runs_For_That_Exception();
		void Specific_Handler_Does_Not_Run_For_Other_Exceptions();
		void Specific_Handler_Runs_For_That_Exception();
	}
}