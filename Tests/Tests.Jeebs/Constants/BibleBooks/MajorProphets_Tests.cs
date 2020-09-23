﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class MajorProphets_Tests
	{
		[Fact]
		public void Returns_MajorProphets_Books()
		{
			// Arrange
			var prophets = "[\"Isaiah\",\"Jeremiah\",\"Lamentations\",\"Ezekiel\",\"Daniel\"]";

			// Act
			var result = F.JsonF.Serialise(BibleBooks.MajorProphets);

			// Assert
			Assert.Equal(prophets, result);
		}
	}
}
