using Autofac;
using Showveo.Data.Repositories;

namespace Showveo.Container
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

            RegisterData();

        	DR.Register(_builder.Build());

        	_hasLoaded = true;
        }
        #endregion

        #region Private Methods
		/// <summary>
		/// Registers the data access components.
		/// </summary>
        private static void RegisterData()
		{
			_builder.RegisterAssemblyTypes(typeof (UserRepository).Assembly).Where(x => x.BaseType == typeof (Repository)).AsImplementedInterfaces();

			
		}
        #endregion
    }
}