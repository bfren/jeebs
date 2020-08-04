namespace Jeebs.SingleTests
{
	public interface ISingle_Single
	{
		void Custom_Input_Multiple_Items_Returns_IError();
		void Custom_Input_One_Item_Returns_Single();
		void IEnumberable_Input_Multiple_Items_Returns_IError();
		void IEnumerable_Input_One_Item_Returns_Single();
		void Incorrect_Subtype_Returns_IError();
		void List_Input_Multiple_Items_Returns_IError();
		void List_Input_One_Item_Returns_Single();
		void Not_IEnumerable_Input_Returns_IError();
	}
}