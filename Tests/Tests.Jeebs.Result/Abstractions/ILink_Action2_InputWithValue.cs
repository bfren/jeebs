namespace Tests.Jeebs.Result_old.Link
{
	public interface ILink_Action2_InputWithValue
	{
		void Successful_Returns_OkV();
		void Unsuccessful_Adds_Exception_Message();
		void Unsuccessful_Returns_Error();
		void Unsuccessful_Then_SkipsAhead();
	}
}