
namespace Jeebs.Data.Clients.SqlServer
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		public long Version { get; set; }
	}
}
