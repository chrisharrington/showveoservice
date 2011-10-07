using System;
using System.Collections.Generic;
using System.Linq;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Service.Encoding
{
	/// <summary>
	/// Creates encoders.
	/// </summary>
	public class EncoderFactory : IEncoderFactory
	{
		#region Data Members
		/// <summary>
		/// The application configuration.
		/// </summary>
		private readonly IConfigurationProvider _configuration;
		#endregion

		#region Properties
		/// <summary>
		/// Returns the number of available encoders.
		/// </summary>
		public int EncoderCount
		{
			get { return CreateAll().Count(); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public EncoderFactory(IConfigurationProvider configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");

			_configuration = configuration;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates an encoder based on a preset.
		/// </summary>
		/// <param name="preset">The preset used to generate the appropriate encoder.</param>
		/// <returns>The created encoder.</returns>
		public IEncoder Create(EncodingPreset preset)
		{
			switch (preset)
			{
				case EncodingPreset.Phone: return new PhoneHandbrakeEncoder(_configuration);
				case EncodingPreset.TV: return new TVHandbrakeEncoder(_configuration);
				case EncodingPreset.Tablet: return new TabletHandbrakeEncoder(_configuration);
			}

			throw new ArgumentException("The preset " + preset + " has no implementation.");
		}

		/// <summary>
		/// Creates encoders for all presets.
		/// </summary>
		/// <returns>The collection of all encoders.</returns>
		public IEnumerable<IEncoder> CreateAll()
		{
			return new List<IEncoder>
			        {
			            new PhoneHandbrakeEncoder(_configuration),
			            new TVHandbrakeEncoder(_configuration),
			            new TabletHandbrakeEncoder(_configuration)
			        };
		}
		#endregion
	}
}