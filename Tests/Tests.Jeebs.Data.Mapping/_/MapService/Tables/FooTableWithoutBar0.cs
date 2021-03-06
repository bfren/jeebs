
namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooTableWithoutBar0 : Table
	{
		public string FooId { get; } = "foo_id";

		public string Bar1 { get; } = "foo_bar1";

		public FooTableWithoutBar0() : base("foo_without_bar0") { }
	}
}
