namespace ShowveoService.Entities
{
	public class UncategorizedMovie
	{
		public virtual int ID { get; set; }
		public virtual string EncodedFile { get; set; }
		public virtual string OriginalFile { get; set; }
	}
}