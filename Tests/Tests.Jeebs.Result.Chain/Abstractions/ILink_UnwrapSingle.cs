namespace Jeebs.LinkTests
{
	public interface ILink_UnwrapSingle
	{
		void Custom_Input_Multiple_Items_Returns_IError();
		void Custom_Input_One_Item_Returns_Single();
		void IEnumberable_Input_Multiple_Items_Returns_IError();
		void IEnumerable_Input_One_Item_Returns_Single();
		void IEnumerable_Input_Incorrect_Subtype_Returns_IError();
		void List_Input_Multiple_Items_Returns_IError();
		void List_Input_One_Item_Returns_Single();
		void Not_IEnumerable_Or_Same_Type_Input_Returns_IError();
		void Other_Input_Same_Type_Returns_Input();
	}
}