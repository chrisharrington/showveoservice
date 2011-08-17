using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ShowveoService.Data.Repositories;
using ShowveoService.MVCApplication.Controllers;

namespace ShowveoService.MVCApplication.Load
{
    /// <summary>
    /// A class used to begin the loading of the application.
    /// </summary>
    public static class Loader
    {
        #region Data Members
        /// <summary>
        /// A flag indicating whether or not the loader has been executed.
        /// </summary>
        private static bool _hasLoaded;

		/// <summary>
		/// The builder object used to build the container at load.
		/// </summary>
    	private static readonly ContainerBuilder _builder;
        #endregion

        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        static Loader()
        {
            _hasLoaded = false;
			_builder = new ContainerBuilder();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Begins the load process.
        /// </summary>
        public static void Start()
        {
            if (_hasLoaded)
                return;

			_builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly).Where(x => x.BaseType == typeof(Repository)).AsImplementedInterfaces();
        	_builder.RegisterControllers(typeof (UserController).Assembly);

        	var container = _builder.Build();
			DR.Register(container);
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        	_hasLoaded = true;
        }
        #endregion
    }
}