using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Autofac;
using Autofac.Integration.Mvc;
using FM.WebSync.Core;
using ShowveoService.Data.Repositories;
using ShowveoService.MVCApplication.Controllers;
using ShowveoService.Service.Configuration;
using ShowveoService.Service.Encoding;
using ShowveoService.Service.File;
using ShowveoService.Service.Logging;
using ShowveoService.Web.Push;
using ShowveoService.Web.Routing;

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
			try
			{
				Logger.Info("Container building...");

				if (_hasLoaded)
					return;

				var container = BuildContainer();
				DR.Register(container);
				DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

				Watch(container);
				OpenPersistentChannels(container);

				Logger.Info("Container built.");

				_hasLoaded = true;
			}
			catch (Exception ex)
			{
				Logger.Error("Error occurred while building the container.", ex);
				throw;
			}
        }
    	#endregion

		#region Private Methods
		/// <summary>
		/// Builds the dependency container.
		/// </summary>
		/// <returns>The built container.</returns>
		private static IContainer BuildContainer()
		{
			_builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly).Where(x => x.BaseType == typeof(Repository)).AsImplementedInterfaces();
			_builder.RegisterAssemblyTypes(typeof(RouteManager).Assembly).AsImplementedInterfaces();
			_builder.RegisterType<WebSyncEncodingProgressPusher>().AsImplementedInterfaces().SingleInstance();
			_builder.RegisterAssemblyTypes(typeof (FolderWatcher).Assembly).AsImplementedInterfaces();
			_builder.RegisterControllers(typeof (UserController).Assembly);
			return _builder.Build();
		}

		/// <summary>
		/// Begins watching a folder for movie file additions.
		/// </summary>
		private static void Watch(IContainer container)
		{
			container.Resolve<IFolderWatcher>().WatchForAdditions(container.Resolve<IConfigurationProvider>().WatchedMovieLocation, container.Resolve<IEncoderManager>().Encode, ".avi", ".mpg", ".mp4", ".mkv");
		}

		/// <summary>
		/// Opens channels for persistent client to server communication.
		/// </summary>
		private static void OpenPersistentChannels(IContainer container)
		{
			
		}
		#endregion
	}
}