using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using ShowveoService.Data;
using ShowveoService.Data.Maps;
using ShowveoService.Data.Repositories;
using ShowveoService.Entities;
using ShowveoService.Test.Data;

namespace ShowveoService.Test
{
	[TestFixture]
	public class Test : DataTest
	{
		private Random _random;

		[Test]
		public void Blah()
		{
			//Loader.Start();
			//XmlConfigurator.Configure();

			//var factory = Fluently
			//    .Configure()
			//    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
			//    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
			//    .BuildSessionFactory();

			//var session = factory.OpenSession();
			//SessionProvider.CurrentSession = session;

			//var repo = new UserMovieRepository();
			//var info = repo.GetForUser(new User {ID = 1}).Select(x => x.Movie).ToArray();

			//session.Close();
		}

		[Test]
		public void ResetEmpty()
		{
			Configuration configuration = null;
			var session = Fluently
				.Configure()
				.ExposeConfiguration(x => configuration = x)
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
				.BuildSessionFactory()
				.OpenSession();

			SessionProvider.CurrentSession = session;

			new SchemaExport(configuration).Execute(false, true, false);

			var user = new User { EmailAddress = "chrisharrington99@gmail.com", FirstName = "Chris", Identity = "blah", LastName = "Harrington", Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" };
			using (var transaction = session.BeginTransaction())
			{
				session.Save(user);
				transaction.Commit();
			}

			session.Close();
		}

		[Test]
		public void Reset()
		{
			_random = new Random();

			Configuration configuration = null;
			var session = Fluently
				.Configure()
				.ExposeConfiguration(x => configuration = x)
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>())
				.BuildSessionFactory()
				.OpenSession();

			SessionProvider.CurrentSession = session;

			new SchemaExport(configuration).Execute(false, true, false);

			var user = new User { EmailAddress = "chrisharrington99@gmail.com", FirstName = "Chris", Identity = "blah", LastName = "Harrington", Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" };
			using (var transaction = session.BeginTransaction())
			{
				session.Save(user);
				transaction.Commit();
			}

			using (var transaction = session.BeginTransaction())
			{
				var repository = new UncategorizedMovieRepository();
				repository.Insert(new UncategorizedMovie { OriginalFile = "file1.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file2.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file3.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file4.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file5.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file6.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file7.avi", EncodedFile = "encoded1.mp4" });
				repository.Insert(new UncategorizedMovie { OriginalFile = "file8.avi", EncodedFile = "encoded1.mp4" });
				transaction.Commit();
			}

			var genres = new List<Genre>();
			using (var transaction = session.BeginTransaction())
			{
				var repository = new GenreRepository();
				genres.Add(new Genre { Name = "Action" });
				genres.Add(new Genre { Name = "Comedy" });
				genres.Add(new Genre { Name = "Romance" });
				genres.Add(new Genre { Name = "Drama" });
				genres.Add(new Genre { Name = "Thriller" });
				genres.Add(new Genre { Name = "Pron" });
				foreach (var genre in genres)
					repository.Insert(genre);
				transaction.Commit();
			}

			var people = new List<Person>();
			using (var transaction = session.BeginTransaction())
			{
				var repository = new PersonRepository();
				people.Add(new Person { FirstName = "Brad", LastName = "Pitt" });
				people.Add(new Person { FirstName = "Steve", LastName = "Buscemi" });
				people.Add(new Person { FirstName = "Nicole", LastName = "Kidman" });
				people.Add(new Person { FirstName = "Gwyneth", LastName = "Paltrow" });
				people.Add(new Person { FirstName = "Harrison", LastName = "Ford" });
				people.Add(new Person { FirstName = "Val", LastName = "Kilmer" });
				people.Add(new Person { FirstName = "George", LastName = "Clooney" });
				foreach (var person in people)
					repository.SaveOrUpdate(person);
				transaction.Commit();
			}

			using (var transaction = session.BeginTransaction())
			{
				var repository = new MovieRepository();
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), Producers = GenerateRandomPeople(people), Director = GenerateRandomPeople(people).First(), DateAdded = GenerateRandomDate(), FileLocation = "blah"});
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Moneyball", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/17e/4e4306015e73d6408900017e/moneyball-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Abduction", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/37c/4db301ed5e73d67a8100037c/abduction-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Transformers - Dark of the Moon", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/1ed/4e6fab557b9aa1182c0001ed/transformers-dark-of-the-moon-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "X-Men First Class", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/422/4e5ea6a45e73d6072900c422/x-men-first-class-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Horrible Bosses", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0f0/4e7e24207b9aa15eb80000f0/horrible-bosses-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Drive", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/017/4e827acd5e73d67843000017/drive-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Thor", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/61f/4e8057b65e73d6709300061f/thor-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Star Wars: Episode IV - A New Hope", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/0ae/4bc90145017a3c57fe0000ae/star-wars-episode-iv-a-new-hope-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				repository.Insert(new Movie { Name = "Pirates of the Caribbean: On Stranger Tides", Description = "blah", Year = DateTime.Now.AddYears(-10), PosterLocation = "http://cf1.imgobject.com/posters/970/4e5e9a1b5e73d60b31006970/pirates-of-the-caribbean-on-stranger-tides-original.jpg", Genres = GenerateRandomGenres(genres), Actors = GenerateRandomPeople(people), DateAdded = GenerateRandomDate(), FileLocation = "blah" });
				transaction.Commit();
			}

			using (var transaction = session.BeginTransaction())
			{
				var userMovieRepository = new UserMovieRepository();
				var movieRepository = new MovieRepository();
				foreach (var movie in movieRepository.GetAll())
					userMovieRepository.Insert(new UserMovie {User = user, Movie = movie});
				transaction.Commit();
			}

			session.Close();
		}

		private DateTime GenerateRandomDate()
		{
			return DateTime.Now.AddDays(_random.Next(1, 2000)*-1);
		}

		private IEnumerable<Genre> GenerateRandomGenres(IEnumerable<Genre> genres)
		{
			var begin = genres.First().ID;
			var end = genres.Last().ID;
			var first = _random.Next(begin, end);
			var second = _random.Next(begin, end);
			while (first == second)
				second = _random.Next(begin, end);
			return genres.Where(x => x.ID == first || x.ID == second).ToArray();
		}

		private IEnumerable<Person> GenerateRandomPeople(IEnumerable<Person> people)
		{
			var begin = people.First().ID;
			var end = people.Last().ID;
			var first = _random.Next(begin, end);
			var second = _random.Next(begin, end);
			while (first == second)
				second = _random.Next(begin, end);
			return people.Where(x => x.ID == first || x.ID == second).ToArray();
		}
	}
}