using System;

namespace ShowveoService.Entities
{
	public class EncodingMovieTask
	{
		public Guid ID { get; set; }
		public string File { get; set; }
		public double PercentComplete { get; set; }
	}
}