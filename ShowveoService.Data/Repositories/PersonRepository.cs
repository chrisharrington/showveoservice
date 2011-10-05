using System;
using System.Linq;
using NHibernate.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data.Repositories
{
	/// <summary>
	/// A container for person information.
	/// </summary>
	public class PersonRepository : Repository, IPersonRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a collection of all people.
		/// </summary>
		/// <returns>The collection of person information.</returns>
		public IQueryable<Person> GetAll()
		{
			return CurrentSession.Query<Person>();
		}

		/// <summary>
		/// Inserts a person into the repository.
		/// </summary>
		/// <param name="person">The person to insert.</param>
		public void Insert(Person person)
		{
			if (person == null)
				throw new ArgumentNullException("person");

			CurrentSession.Save(person);
		}
		#endregion
	}
}