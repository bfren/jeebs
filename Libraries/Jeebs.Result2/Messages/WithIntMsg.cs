namespace Jm
{
	/// <summary>
	/// Special case: Integer value
	/// </summary>
	public class WithIntMsg : WithValueMsg<int>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">Integer</param>
		public WithIntMsg(int value) : base(value) { }
	}
}
