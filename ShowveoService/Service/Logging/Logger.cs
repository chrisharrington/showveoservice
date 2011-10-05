using System;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace ShowveoService.Service.Logging
{
	/// <summary>
	/// A class used to keep track of warnings and errors as they occur.
	/// </summary>
	public static class Logger
	{
		#region Data Members
		/// <summary>
		/// The log4net logger.
		/// </summary>
		private static ILog _log;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		static Logger()
		{
			_log = LogManager.GetLogger("logger");
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Posts an informational message to the log.
		/// </summary>
		/// <param name="info">The informational message.</param>
		public static void Info(string info )
		{
			_log.Info(info);
		}

		/// <summary>
		/// Posts a warning to the logger.
		/// </summary>
		/// <param name="warning">The warning to post.</param>
		/// <param name="ex">The optional exception.</param>
		public static void Warn(string warning, Exception ex = null)
		{
			if (ex == null)
				_log.Warn(warning);
			else
				_log.Warn(ex);
		}

		/// <summary>
		/// Posts an error to the logger.
		/// </summary>
		/// <param name="error">The error to post.</param>
		/// <param name="ex">The optional exception.</param>
		public static void Error(string error, Exception ex = null)
		{
			if (ex == null)
				_log.Error(error);
			else
				_log.Error(error, ex);
		}
		#endregion
	}
}