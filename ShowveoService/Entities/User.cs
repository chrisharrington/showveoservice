namespace ShowveoService.Entities
{
	/// <summary>
	/// A user that can log in to ShowveoService.
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

		#region Public Methods
		public virtual bool Equals(User other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.ID == ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (User)) return false;
			return Equals((User) obj);
		}

		public override int GetHashCode()
		{
			return ID;
		}
		#endregion
	}
}