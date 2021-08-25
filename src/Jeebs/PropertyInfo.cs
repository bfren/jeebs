// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Reflection;

namespace Jeebs
{
	/// <summary>
	/// Dynamically gets and sets property values on an object
	/// </summary>
	/// <typeparam name="TObject">Object type</typeparam>
	/// <typeparam name="TProperty">Property type</typeparam>
	public class PropertyInfo<TObject, TProperty>
	{
		/// <summary>
		/// PropertyInfo object
		/// </summary>
		private readonly PropertyInfo info;

		/// <summary>
		/// Return the property name
		/// </summary>
		public string Name =>
			info.Name;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="info">PropertyInfo object</param>
		public PropertyInfo(PropertyInfo info) =>
			this.info = info;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="propertyName">Property name</param>
		public PropertyInfo(string propertyName)
		{
			if (typeof(TObject).GetProperty(propertyName) is PropertyInfo info)
			{
				if (typeof(TProperty).Equals(info.PropertyType))
				{
					this.info = info;
				}
				else
				{
					throw new InvalidOperationException($"The property '{propertyName}' is not of type {typeof(TProperty)}.");
				}
			}
			else
			{
				throw new InvalidOperationException($"'{propertyName}' is not a valid property of {typeof(TObject)}.");
			}
		}

		/// <summary>
		/// Get the value of the property from the specified object
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>Property value</returns>
		public TProperty Get(TObject obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			if (info.GetValue(obj, null) is TProperty value)
			{
				return value;
			}

			throw new InvalidOperationException($"Unable to get value of property '{info.Name}' from type {typeof(TObject)} - the value has not been set.");
		}

		/// <summary>
		/// Set the value of the property on the specified object
		/// </summary>
		/// <param name="obj">Object</param>
		/// <param name="value">Value</param>
		public void Set(TObject obj, TProperty value)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			info.SetValue(obj, value);
		}
	}
}
