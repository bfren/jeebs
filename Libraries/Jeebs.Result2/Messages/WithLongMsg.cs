namespace Jm
{
	/// <summary>
	/// Special case: 64-bit Integer value
	/// </summary>
	public class WithLongMsg : WithValueMsg<long>
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="value">64-bit integer</param>
		public WithLongMsg(long value) : base(value) { }
	}
}
