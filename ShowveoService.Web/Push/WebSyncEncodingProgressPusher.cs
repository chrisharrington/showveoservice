using System;
using System.Web.Script.Serialization;
using FM.WebSync.Core;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Web.Push
{
	/// <summary>
	/// A class used to push out information pertaining to encoding tasks.
	/// </summary>
	public class WebSyncEncodingProgressPusher : IEncodingProgressPusher
	{
		#region Data Members
		/// <summary>
		/// The publisher used to send messages to subscribed clients.
		/// </summary>
		private readonly Publisher _publisher;

		/// <summary>
		/// The channel used to determine the location to send a published message.
		/// </summary>
		private readonly string _channel;

		/// <summary>
		/// Serializes objects into json.
		/// </summary>
		private readonly JavaScriptSerializer _serializer;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public WebSyncEncodingProgressPusher(IConfigurationProvider configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");

			_channel = configuration.EncodingChannel;
			_publisher = new Publisher(new PublisherArgs { RequestUrl = configuration.WebSyncRequestUrl });
			_serializer = new JavaScriptSerializer();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Pushes information regarding the progress of an encoding movie task.
		/// </summary>
		/// <param name="task">The encoding movie task.</param>
		public void Push(EncodingMovieTask task)
		{
			if (task == null)
				throw new ArgumentNullException("task");

			var blah = _publisher.Publish(new Publication {
				Channel = _channel,
				DataJson = _serializer.Serialize(task)
			});
		}
		#endregion
	}
}