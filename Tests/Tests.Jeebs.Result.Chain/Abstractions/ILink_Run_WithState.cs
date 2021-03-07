// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Link_Tests.WithState
{
	public interface ILink_Run_WithState : ILink_Run
	{
		void IOk_ValueType_WithState_Input_When_IError_Returns_IError();
		void IOk_ValueType_WithState_Input_When_IOk_Catches_Exception();
		void IOk_ValueType_WithState_Input_When_IOk_Runs_Action();
		void IOk_Value_WithState_Input_When_IError_Returns_IError();
		void IOk_Value_WithState_Input_When_IOk_Catches_Exception();
		void IOk_Value_WithState_Input_When_IOk_Runs_Action();
	}
}