// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.LinkExtensions_Tests
{
	public interface ILinkExtensions_Catch
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