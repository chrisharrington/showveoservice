using FluentNHibernate.Mapping;
using Showveo.Entities;

namespace Showveo.Data.Maps
{
	/// <summary>
	/// Maps the NHibernate User entity.
	/// </summary>
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Id(x => x.ID);

			Map(x => x.EmailAddress);
			Map(x => x.FirstName);
			Map(x => x.Identity);
			Map(x => x.LastName);
			Map(x => x.Password);
		}
	}
}