namespace Jeebs.LinkTests.WithState
{
	public interface ILink_Map_WithState
	{
		void IOk_ValueType_WithState_Input_When_IError_Returns_IError();
		void IOk_ValueType_WithState_Input_When_IOk_Catches_Exception();
		void IOk_ValueType_WithState_Input_When_IOk_Maps_To_Next_Type();
		void IOk_Value_WithState_Input_When_IError_Returns_IError();
		void IOk_Value_WithState_Input_When_IOk_Catches_Exception();
		void IOk_Value_WithState_Input_When_IOk_Maps_To_Next_Type();
	}
}