// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;

namespace Jeebs.WordPress.Entities;

public abstract class Id_Tests<TEntity, TId>
	where TEntity : IWithId<TId>, new()
	where TId : IStrongId
{
	public abstract void Test00_Id_Returns_Database_Id();

	protected void Test00(Func<long, TEntity> create)
	{
		// Arrange
		var value = Rnd.Lng;
		var entity = create(value);

		// Act
		var result = entity.Id;

		// Assert
		Assert.Equal(value, result.Value);
	}
}
