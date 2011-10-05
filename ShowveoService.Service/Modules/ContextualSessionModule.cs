using System;
using System.Web;
using ShowveoService.Data;

namespace ShowveoService.Service.Modules
{
	/// <summary>
	/// An http module used to provide NHibernate sessions based on request begin and end.
	/// </summary>
	public class ContextualSessionModule : IHttpModule
	{
		#region Public Methods
		/// <summary>
		/// Initializes a module and prepares it to handle requests.
		/// </summary>
		public void Init(HttpApplication context)
		{
			context.BeginRequest += OnBeginRequest;
			context.EndRequest += OnEndRequest;
		}

		/// <summary>
		/// Disposes of the resources used by the module.
		/// </summary>
		public void Dispose() {}
		#endregion

		#region Private Methods
		/// <summary>
		/// Fired when the request begins.
		/// </summary>
		private static void OnBeginRequest(object sender, EventArgs e)
		{
			SessionProvider.Open(((HttpApplication) sender).Context);
		}

		/// <summary> 
		/// Fired when the request ends.
		/// </summary>
		private static void OnEndRequest(object sender, EventArgs e)
		{
			SessionProvider.Close(((HttpApplication)sender).Context);
		}
		#endregion
	}
}