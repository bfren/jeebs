using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database Entity with Custom ID Property
	/// </summary>
	public abstract class Entity : IEntity
	{
		/// <summary>
		/// Entity Id
		/// </summary>
		public virtual long Id { get => IdFactory.Value; }

		/// <summary>
		/// Id Factory
		/// </summary>
		private Lazy<long> IdFactory { get; }

		/// <summary>
		/// Create object with an Id of -1
		/// </summary>
		protected Entity() => IdFactory = new Lazy<long>(-1);

		/// <summary>
		/// Use expression to return Entity ID
		/// </summary>
		/// <param name="idProperty">Expression to return ID Property</param>
		protected Entity(Expression<Func<Entity, long>> idProperty)
		{
			IdFactory = new Lazy<long>(() => idProperty.Compile().Invoke(this));
		}
	}
}
