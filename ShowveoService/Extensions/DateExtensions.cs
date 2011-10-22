using System;

namespace ShowveoService.Extensions
{
	/// <summary>
	/// A class providing extension methods for the DateTime class.
	/// </summary>
	public static class DateExtensions
	{
		#region Public Methods
		/// <summary>
		/// Returns the number of milliseconds since epoch time, which is January 1st, 1970.
		/// </summary>
		/// <param name="source">The date to examine.</param>
		/// <returns>The calculated number of milliseconds.</returns>
		public static double SecondsSinceEpoch(this DateTime source)
		{
			return (source - new DateTime(1970, 1, 1)).TotalSeconds;
		}
		#endregion
	}
}