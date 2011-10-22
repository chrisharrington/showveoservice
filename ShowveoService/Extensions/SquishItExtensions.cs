using System;
using System.IO;
using System.Linq;
using System.Web;
using SquishIt.Framework.JavaScript;

namespace ShowveoService.Extensions
{
	/// <summary>
	/// Extends SquishIt functionality.
	/// </summary>
	public static class SquishItExtensions
	{
		#region Public Methods
		/// <summary>
		/// Recursively adds the javascript contents of a folder to the bundle.
		/// </summary>
		/// <param name="bundle">The bundle to which javascript files should be added.</param>
		/// <param name="folder">The folder to recursively search through.</param>
		/// <returns>The modified bundle.</returns>
		public static IJavaScriptBundleBuilder AddFolder(this IJavaScriptBundle bundle, string folder)
		{
			if (string.IsNullOrEmpty(folder))
				throw new ArgumentNullException("folder");

			folder = HttpContext.Current.Server.MapPath(folder);

			if (!Directory.Exists(folder))
				throw new FileNotFoundException("The directory \"" + folder + "\" corresponds to no directory.");

			return RecursivelyAddFolder(bundle, null, folder);
		}

		/// <summary>
		/// Recursively adds the javascript contents of a folder to the bundle.
		/// </summary>
		/// <param name="bundle">The bundle to which javascript files should be added.</param>
		/// <param name="folder">The folder to recursively search through.</param>
		/// <returns>The modified bundle.</returns>
		public static IJavaScriptBundleBuilder AddFolder(this IJavaScriptBundleBuilder bundle, string folder)
		{
			if (string.IsNullOrEmpty(folder))
				throw new ArgumentNullException("folder");

			folder = HttpContext.Current.Server.MapPath(folder);

			if (!Directory.Exists(folder))
				throw new FileNotFoundException("The directory \"" + folder + "\" corresponds to no directory.");

			return RecursivelyAddFolder(null, bundle, folder);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Iterates through the files and folders of the given folder and adds any javascript files found.
		/// </summary>
		/// <param name="bundle">The bundle to which javascript files should be added.</param>
		/// <param name="builder">The builder used for everything except the first iteration...</param>
		/// <param name="folder">The folder to search through.</param>
		/// <returns>The modified bundle.</returns>
		private static IJavaScriptBundleBuilder RecursivelyAddFolder(IJavaScriptBundle bundle, IJavaScriptBundleBuilder builder, string folder)
		{
			foreach (var file in Directory.GetFiles(folder).Where(x => Path.GetExtension(x) == ".js"))
				builder = builder == null ? bundle.Add(TranslatePath(file)) : builder.Add(TranslatePath(file));
			foreach (var localFolder in Directory.GetDirectories(folder))
				builder = RecursivelyAddFolder(bundle, builder, localFolder);
			return builder;
		}

		/// <summary>
		/// Translates a file path to a url.
		/// </summary>
		/// <param name="file">The file path.</param>
		/// <returns>The resulting url.</returns>
		private static string TranslatePath(string file)
		{
			var apppath = HttpContext.Current.Server.MapPath("~");
			var vdir = "/" + HttpRuntime.AppDomainAppVirtualPath.Replace("/", "");
			return vdir + file.Replace(apppath, "").Replace("\\", "/");
		}
		#endregion
	}
}