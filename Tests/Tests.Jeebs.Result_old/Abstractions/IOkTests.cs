namespace Jeebs_old
{
	public interface IOkTests
	{
		void OkNew_Different_Type_Adds_Messages_Params_And_Returns_New_Ok();
		void OkNew_Different_Type_Adds_Message_ByType_And_Returns_Ok();
		void OkNew_Different_Type_Adds_Message_OfType_And_Returns_Ok();
		void OkNew_Same_Type_Adds_Messages_Params_And_Returns_Ok();
		void OkNew_Same_Type_Adds_Message_ByType_And_Returns_Ok();
		void OkNew_Same_Type_Adds_Message_OfType_And_Returns_Ok();
		void OkV_Different_Type_Sets_Value_Adds_Messages_Params_And_Returns_OkV();
		void OkV_Different_Type_Sets_Value_Adds_Message_ByType_And_Returns_OkV();
		void OkV_Different_Type_Sets_Value_Adds_Message_OfType_And_Returns_OkV();
		void OkV_Same_Type_Sets_Value_Adds_Messages_Params_And_Returns_OkV();
		void OkV_Same_Type_Sets_Value_Adds_Message_ByType_And_Returns_OkV();
		void OkV_Same_Type_Sets_Value_Adds_Message_OfType_And_Returns_OkV();
		void Ok_Adds_Messages_Params_And_Returns_Ok();
		void Ok_Adds_Message_ByType_And_Returns_Ok();
		void Ok_Adds_Message_OfType_And_Returns_Ok();
	}
}