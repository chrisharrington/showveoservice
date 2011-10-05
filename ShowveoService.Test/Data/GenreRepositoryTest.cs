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
	/// Tests the GenreRepository class.
	/// </summary>
	[TestFixture]
	public class GenreRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IGenreRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the test.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new GenreRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid genre.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidGenreOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a genre by calling Insert, then querying the session
		/// to ensure that the genre was actually inserted.
		/// </summary>
		[Test]
		public void ShouldRetrieveInsertedGenreAfterInsert()
		{
			var genre = new Genre {Name = "bromance"};
			_sut.Insert(genre);

			var retrieved = InMemorySession.Query<Genre>().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().Name, genre.Name);
		}

		/// <summary>
		/// Tests that the GetAll method successfully retrieves previously inserted genres.
		/// </summary>
		[Test]
		public void ShouldRetrievePreviouslyInsertedGenresOnGetAll()
		{
			var genre = new Genre {Name = "bromance"};
			using (var transaction = InMemorySession.BeginTransaction())
			{
				InMemorySession.Save(genre);
				transaction.Commit();
			}

			var retrieved = _sut.GetAll().ToArray();
			Assert.AreEqual(retrieved.Length, 1);
			Assert.AreEqual(retrieved.First().Name, genre.Name);
		}
		#endregion
	}
}