using System;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;
using ShowveoService.Data;
using ShowveoService.Data.Repositories;
using ShowveoService.Entities;

namespace ShowveoService.Test.Data
{
	/// <summary>
	/// Tests the PersonRepository class.
	/// </summary>
	[TestFixture]
	public class PersonRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IPersonRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the test.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new PersonRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid person.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidPersonOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a person by calling Insert, then querying the session
		/// to ensure that the person was actually inserted.
		/// </summary>
		[Test]
		public void ShouldRetrieveInsertedPersonAfterInsert()
		{
			var person = new Person { FirstName = "steve", LastName = "blah" };
			_sut.Insert(person);

			var retrieved = InMemorySession.Query<Person>().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().FirstName, person.FirstName);
		}

		/// <summary>
		/// Tests that the GetAll method successfully retrieves previously inserted persons.
		/// </summary>
		[Test]
		public void ShouldRetrievePreviouslyInsertedPersonsOnGetAll()
		{
			var person = new Person { FirstName = "steve", LastName = "blah" };
			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(person);
				transaction.Commit();
			}

			var retrieved = _sut.GetAll().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().FirstName, person.FirstName);
		}
		#endregion
	}
}