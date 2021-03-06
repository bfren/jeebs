
namespace Jeebs.R_Tests
{
	public interface IR_Error
	{
		void Different_Type_Keeps_Messages();
		void Different_Type_Returns_Error_Of_Different_Type();
		void Same_Type_Returns_Error_Of_Type();
		void Without_Type_Returns_Error_Of_Type();
	}
}