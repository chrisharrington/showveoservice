namespace Showveo.Entities
{
	/// <summary>
	/// A user that can log in to Showveo.
	/// </summary>
	public class User
	{
		#region Properties
		public virtual int ID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string Identity { get; set; }
		public virtual string Password { get; set; }
		#endregion
	}
}