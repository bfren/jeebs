namespace Jeebs_old.LinkMap
{
	public interface ILinkMap_Func1_InputWithoutValue
	{
		void Successful_Returns_Ok();
		void Unsuccessful_Adds_Exception_Message();
		void Unsuccessful_Returns_Error();
		void Unsuccessful_Then_SkipsAhead();
	}
}