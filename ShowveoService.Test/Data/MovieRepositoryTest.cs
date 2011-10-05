using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;
using ShowveoService.Data;
using ShowveoService.Data.Repositories;
using ShowveoService.Entities;

namespace ShowveoService.Test.Data
{
	/// <summary>
	/// Tests the MovieRepository class.
	/// </summary>
	[TestFixture]
	public class MovieRepositoryTest : DataTest
	{
		#region Data Members
		/// <summary>
		/// The subject under test.
		/// </summary>
		private IMovieRepository _sut;
		#endregion

		#region Setup
		/// <summary>
		/// Sets up the tests.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			SessionProvider.CurrentSession = InMemorySession;
			_sut = new MovieRepository();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Tests that the Insert method fails gracefully when given an invalid movie.
		/// </summary>
		[Test]
		public void ShouldFailWithInvalidMovieOnInsert()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Insert(null));
		}

		/// <summary>
		/// Tests that the Insert method successfully inserts a movie.
		/// </summary>
		[Test]
		public void ShouldInsertMovie()
		{
			var movie = new Movie {Name = "testmovie", Description = "testdescription", Year = DateTime.Now.AddYears(-10), FileLocation = "file", PosterLocation = "poster"};
			_sut.Insert(movie);

			var retrieved = InMemorySession.Query<Movie>().First();
			Assert.AreEqual(retrieved.Name, movie.Name);
		}

		/// <summary>
		/// Tests that the GetAll method successfully retrieves all movies that exist in the repository.
		/// </summary>
		[Test]
		public void ShouldGetAll()
		{
			var movies = new List<Movie>();
			movies.Add(new Movie {Name = "first", Description = "firstdescription", Year = DateTime.Now, FileLocation = "file", PosterLocation = "poster"});
			movies.Add(new Movie { Name = "second", Description = "secondtdescription", Year = DateTime.Now, FileLocation = "file", PosterLocation = "poster" });
			movies.Add(new Movie { Name = "third", Description = "thirddescription", Year = DateTime.Now, FileLocation = "file", PosterLocation = "poster" });

			using (var transaction = InMemorySession.BeginTransaction())
			{
				foreach (var movie in movies)
					InMemorySession.Save(movie);
				transaction.Commit();
			}

			var retrieved = _sut.GetAll().ToArray();
			Assert.AreEqual(retrieved.Length, movies.Count);
			Assert.IsNotNull(retrieved.Where(x => x.Name == movies[0].Name));
			Assert.IsNotNull(retrieved.Where(x => x.Name == movies[1].Name));
			Assert.IsNotNull(retrieved.Where(x => x.Name == movies[2].Name));
		}
		#endregion
	}
}