using System;
using ShowveoService.Entities;

namespace ShowveoService.Service.Presets
{
	/// <summary>
	/// A class used to retrieve the indicator for a preset.
	/// </summary>
	public static class PresetIndicator
	{
		#region Public Methods
		public static string Get(Preset preset)
		{
			switch (preset)
			{
				case Preset.Phone: return ".phone";
				case Preset.Tablet: return ".tablet";
				case Preset.TV: return ".tv";
			}

			throw new InvalidOperationException("No indicator exists for preset \"" + preset + "\".");
		}
		#endregion
	}
}