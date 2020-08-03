namespace Jeebs_old
{
	public interface IErrorTests
	{
		void ErrorNew_Different_Type_Adds_Messages_Params_And_Returns_Error();
		void ErrorNew_Different_Type_Adds_Message_ByType_And_Returns_Error();
		void ErrorNew_Different_Type_Adds_Message_OfType_And_Returns_Error();
		void ErrorNew_Same_Type_Adds_Messages_Params_And_Returns_Error();
		void ErrorNew_Same_Type_Adds_Message_ByType_And_Returns_Error();
		void ErrorNew_Same_Type_Adds_Message_OfType_And_Returns_Error();
		void Error_Adds_Messages_Params_And_Returns_Error();
		void Error_Adds_Message_ByType_And_Returns_Error();
		void Error_Adds_Message_OfType_And_Returns_Error();
	}
}