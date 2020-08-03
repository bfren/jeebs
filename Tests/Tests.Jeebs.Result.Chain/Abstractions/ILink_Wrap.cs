namespace Jeebs.LinkTests
{
	public interface ILink_Wrap
	{
		void Func_Input_When_IError_Returns_IError();
		void Func_Input_When_IOk_Catches_Exception_Returns_IError();
		void Func_Input_When_IOk_Wraps_Value();
		void Value_Input_When_IError_Returns_IError();
		void Value_Input_When_IOk_Wraps_Value();
	}
}