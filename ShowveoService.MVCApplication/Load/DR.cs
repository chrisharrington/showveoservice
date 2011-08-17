using System;
using Autofac;

namespace ShowveoService.MVCApplication.Load
{
	/// <summary>
	/// The dependency resolver used to retrieve required classes.
	/// </summary>
	public static class DR
	{
		#region Data Members
		/// <summary>
		/// The underlying Autofac container.
		/// </summary>
		private static IContainer _container;
		#endregion

		#region Public Methods
		/// <summary>
		/// Registers an Autofac container with the dependency resolver.
		/// </summary>
		/// <param name="container">The Autofac container to register.</param>
		public static void Register(IContainer container)
		{
			if (container == null)
				throw new ArgumentNullException("container");

			_container = container;
		}

		/// <summary>
		/// Resolves a dependency with the given interface type.
		/// </summary>
		/// <typeparam name="TDependency">The dependency's interface type.</typeparam>
		/// <returns>The resolved dependency or null.</returns>
		public static TDependency Get<TDependency>()
		{
			return _container.Resolve<TDependency>();
		}
		#endregion
	}
}