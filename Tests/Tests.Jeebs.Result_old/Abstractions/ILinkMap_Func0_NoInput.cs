namespace Tests.Jeebs.Result_old.LinkMap
{
	public interface ILinkMap_Func0_NoInput
	{
		void Successful_Returns_OkWithValue();
		void Unsuccessful_Adds_Exception_Message();
		void Unsuccessful_Returns_Error();
		void Unsuccessful_Then_SkipsAhead();
	}
}