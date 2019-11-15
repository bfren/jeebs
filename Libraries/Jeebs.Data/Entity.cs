using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Data
{
	public abstract class Entity : IEntity
	{
		/// <summary>
		/// Entity Id
		/// </summary>
		public virtual int Id
		{
			get { return IdFactory.Value; }
		}

		/// <summary>
		/// Id Factory
		/// </summary>
		private Lazy<int> IdFactory { get; }

		/// <summary>
		/// Create object with an Id of -1
		/// </summary>
		protected Entity() => IdFactory = new Lazy<int>(-1);

		/// <summary>
		/// Use expression to return Entity ID
		/// </summary>
		/// <param name="idProperty">Expression to return ID Property</param>
		protected Entity(Expression<Func<Entity, int>> idProperty)
		{
			IdFactory = new Lazy<int>(() => idProperty.Compile().Invoke(this));
		}
	}
}
