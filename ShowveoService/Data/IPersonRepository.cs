using System.Linq;
using ShowveoService.Entities;

namespace ShowveoService.Data
{
	/// <summary>
	/// Defines a container for person information.
	/// </summary>
	public interface IPersonRepository
	{
		#region Public Methods
		/// <summary>
		/// Retrieves a person by name.
		/// </summary>
		/// <param name="name">The name of the person.</param>
		/// <returns>The retrieved person or null.</returns>
		Person GetByName(string name);

		/// <summary>
		/// Retrieves a collection of all people.
		/// </summary>
		/// <returns>The collection of person information.</returns>
		IQueryable<Person> GetAll();

		/// <summary>
		/// Inserts or updates a person.
		/// </summary>
		/// <param name="person">The person to persist.</param>
		void SaveOrUpdate(Person person);
		#endregion
	}
}