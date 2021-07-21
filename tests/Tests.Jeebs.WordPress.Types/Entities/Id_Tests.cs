﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Xunit;

namespace Jeebs.WordPress.Data.Entities
{
	public abstract class Id_Tests<TEntity, TId>
		where TEntity : IWithId<TId>, new()
		where TId : StrongId
	{
		public abstract void Test00_Id_Returns_Database_Id();

		protected void Test00(Func<ulong, TEntity> create)
		{
			// Arrange
			var value = F.Rnd.Ulng;
			var entity = create(value);

			// Act
			var result = entity.Id;

			// Assert
			Assert.Equal(value, result.Value);
		}
	}
}
