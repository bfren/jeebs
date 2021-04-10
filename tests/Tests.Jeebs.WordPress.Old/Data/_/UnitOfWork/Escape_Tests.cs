// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.WordPress.Data.UnitOfWork_Tests
{
	public class Escape_Tests
	{
		[Fact]
		public void Single_Element_Calls_Adapter_SplitAndEscape()
		{
			// Arrange
			var (w, _, _, adapter, _, _) = GetUnitOfWork();

			const int vInt = 18;
			var vString = vInt.ToString();

			// Act
			_ = w.Escape(vInt);

			// Assert
			adapter.Received().SplitAndEscape(vString);
		}

		[Fact]
		public void Multiple_Elements_Calls_EscapeAndJoin()
		{
			// Arrange
			var (w, _, _, adapter, _, _) = GetUnitOfWork();

			const string? s0 = "one";
			const string? s1 = "two";
			const string? s2 = "three";

			// Act
			_ = w.Escape(s0, s1, s2);

			// Assert
			adapter.Received().EscapeAndJoin(s0, s1, s2);
		}
	}
}
