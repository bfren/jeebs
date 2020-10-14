﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Rollback_Tests
	{
		[Fact]
		public void Calls_Transaction_Rollback()
		{
			// Arrange
			var (w, _, transaction, _, _, _) = GetUnitOfWork();

			// Act
			w.Rollback();

			// Assert
			transaction.Received().Rollback();
		}

		[Fact]
		public void Catches_And_Logs_Commit_Exception()
		{
			// Arrange
			var (w, _, transaction, _, log, _) = GetUnitOfWork();
			var exception = new Exception();
			transaction.When(t => t.Rollback()).Throw(exception);

			// Act
			w.Rollback();

			// Assert
			log.Received().Error(exception, "Error rolling back transaction.");
		}
	}
}
