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
		/// Retrieves a collection of all people.
		/// </summary>
		/// <returns>The collection of person information.</returns>
		IQueryable<Person> GetAll();

		/// <summary>
		/// Inserts a person into the repository.
		/// </summary>
		/// <param name="person">The person to insert.</param>
		void Insert(Person person);
		#endregion
	}
}