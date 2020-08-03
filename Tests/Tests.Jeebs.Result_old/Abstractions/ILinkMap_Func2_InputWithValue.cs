namespace Jeebs_old.LinkMap
{
	public interface ILinkMap_Func2_InputWithValue
	{
		void Successful_Returns_OkV();
		void Unsuccessful_Adds_Exception_Message();
		void Unsuccessful_Returns_Error();
		void Unsuccessful_Then_SkipsAhead();
	}
}