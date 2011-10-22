using System;
using System.IO;
using System.Web.Mvc;

namespace ShowveoService.MVCApplication.Controllers.Results
{
	public class Video : ActionResult
	{
		#region Data Members
		/// <summary>
		/// The location of the video file.
		/// </summary>
		private readonly string _file;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="file"></param>
		public Video(string file)
		{
			if (string.IsNullOrEmpty(file))
				throw new ArgumentNullException("file");

			_file = file;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
		/// </summary>
		/// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
		public override void ExecuteResult(ControllerContext context)
		{
			using (var stream = new FileStream(_file, FileMode.Open, FileAccess.Read))
			{
				context.HttpContext.Response.AppendHeader("Content-Type", "video/mp4");
				context.HttpContext.Response.AppendHeader("Content-Length", stream.Length.ToString());

				const int buffersize = 16384;
				var buffer = new byte[buffersize];

				int count = stream.Read(buffer, 0, buffersize);
				while (count > 0)
				{
					if (context.HttpContext.Response.IsClientConnected)
					{
						context.HttpContext.Response.OutputStream.Write(buffer, 0, count);
						context.HttpContext.Response.Flush();
						count = stream.Read(buffer, 0, buffersize);
					}
					else
						count = -1;
				}
			}
		}
		#endregion
	}
}