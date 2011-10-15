using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using ShowveoService.Data;
using ShowveoService.Entities;
using ShowveoService.Service.Configuration;

namespace ShowveoService.Web.Remote
{
	/// <summary>
	/// A container for remote movie information.
	/// </summary>
	public class RemoteMovieRepository : IRemoteMovieRepository
	{
		#region Data Members
		/// <summary>
		/// A container for person information.
		/// </summary>
		private readonly IPersonRepository _personRepository;

		/// <summary>
		/// The API key used to access the information stored at themoviedb.org.
		/// </summary>
		private readonly string _apiKey;
		#endregion

		#region Constructors
		/// <summary>
		/// The default constructor.
		/// </summary>
		/// <param name="configuration">The configuration for the application.</param>
		/// <param name="personRepository">A container for person information.</param>
		public RemoteMovieRepository(IConfigurationProvider configuration, IPersonRepository personRepository)
		{
			if (configuration == null)
				throw new ArgumentNullException("configuration");
			if (personRepository == null)
				throw new ArgumentNullException("personRepository");

			_apiKey = configuration.MovieDBAPIKey;
			_personRepository = personRepository;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Performs a search for movie information.
		/// </summary>
		/// <param name="query">The search query.</param>
		/// <returns>The resulting movie information.</returns>
		public IEnumerable<RemoteMovie> Search(string query)
		{
			if (string.IsNullOrEmpty(query))
				throw new ArgumentNullException("query");

			var url = "http://api.themoviedb.org/2.1/Movie.search/en/json/" + _apiKey + "/" + query.Replace(" ", "+");

			var request = WebRequest.Create(url);
			var response = request.GetResponse();
			if (response == null)
				throw new InvalidOperationException("The response for request \"" + url + "\" is null.");

			var stream = response.GetResponseStream();
			if (stream == null)
				throw new InvalidOperationException("The stream for the response for the request at \"" + url + "\" is null.");

			using (var reader = new StreamReader(stream))
			{
				var responseText = reader.ReadToEnd();

				var deserializer = new JavaScriptSerializer();
				return AdaptSearchResults(deserializer.Deserialize<dynamic>(responseText));
			}
		}

		/// <summary>
		/// Retrieves details for a movie.
		/// </summary>
		/// <param name="id">The movie ID.</param>
		/// <returns>The retrieved movie details.</returns>
		public Movie GetDetails(int id)
		{
			var deserializer = new JavaScriptSerializer();
			return AdaptDetails(deserializer.Deserialize<dynamic>("[{\"popularity\":3,\"translated\":true,\"adult\":false,\"language\":\"en\",\"original_name\":\"Rocky IV\",\"name\":\"Rocky IV\",\"alternative_name\":\"Rocky 4\",\"movie_type\":\"movie\",\"id\":1374,\"imdb_id\":\"tt0089927\",\"url\":\"http://www.themoviedb.org/movie/1374\",\"votes\":23,\"rating\":6.6,\"status\":\"Released\",\"tagline\":\"He's facing the ultimate challenge. And fighting for his life.\",\"certification\":\"PG\",\"overview\":\"Rocky must come out of retirement to battle a gargantuan Soviet fighter named Drago, who brutally punished Rocky's friend and former rival, Apollo Creed. Seeking revenge in the name of his fallen comrade and his country, Rocky agrees to fight Drago in Moscow on Christmas, and the bout changes both fighters -- and the world.\",\"keywords\":[\"usa\",\"transporter\",\"loss of lover\",\"cold war\",\"union of soviet socialist republics\",\"boxer\",\"kgb\",\"dying and death\",\"loss of powers\",\"boxing match\",\"training\",\"matter of life and death\",\"sibiria\",\"friendship\",\"rache\",\"victory\"],\"released\":\"1985-11-21\",\"runtime\":91,\"budget\":31000000,\"revenue\":300473716,\"homepage\":\"\",\"trailer\":\"http://www.youtube.com/watch?v=bN-SShi58cI\",\"genres\":[{\"type\":\"genre\",\"url\":\"http://themoviedb.org/genre/action\",\"name\":\"Action\",\"id\":28},{\"type\":\"genre\",\"url\":\"http://themoviedb.org/genre/drama\",\"name\":\"Drama\",\"id\":18},{\"type\":\"genre\",\"url\":\"http://themoviedb.org/genre/sport\",\"name\":\"Sport\",\"id\":9805}],\"studios\":[{\"url\":\"http://www.themoviedb.org/company/114\",\"name\":\"United Artists\",\"id\":114}],\"languages_spoken\":[{\"code\":\"en\",\"name\":\"English\",\"native_name\":\"\"}],\"countries\":[{\"code\":\"US\",\"name\":\"United States of America\",\"url\":\"http://www.themoviedb.org/country/us\"}],\"posters\":[{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1500,\"width\":1000,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-original.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":750,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-mid.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":513,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-w342.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":278,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-cover.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":231,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-w154.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":138,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/979/4e18d6a87b9aa177d0001979/rocky-iv-thumb.jpg\",\"id\":\"4e18d6a87b9aa177d0001979\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1081,\"width\":765,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-original.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":707,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-mid.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":262,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-cover.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":130,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-thumb.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":483,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-w342.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":217,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/d97/4bc91020017a3c57fe005d97/rocky-iv-w154.jpg\",\"id\":\"4bc91020017a3c57fe005d97\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1420,\"width\":1000,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-original.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":710,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-mid.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":263,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-cover.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":131,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-thumb.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":486,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-w342.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":219,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/d83/4bc9101e017a3c57fe005d83/rocky-iv-w154.jpg\",\"id\":\"4bc9101e017a3c57fe005d83\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1175,\"width\":1000,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-original.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":588,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-mid.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":402,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-w342.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":217,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-cover.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":181,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-w154.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":108,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/240/4d76b93d7b9aa15c77000240/rocky-iv-thumb.jpg\",\"id\":\"4d76b93d7b9aa15c77000240\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1500,\"width\":1000,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-original.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":750,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-mid.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":278,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-cover.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":138,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-thumb.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":513,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-w342.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":231,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/d8d/4bc9101f017a3c57fe005d8d/rocky-iv-w154.jpg\",\"id\":\"4bc9101f017a3c57fe005d8d\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1572,\"width\":1000,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-original.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":786,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-mid.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":291,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-cover.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":145,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-thumb.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":538,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-w342.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":242,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/d88/4bc9101e017a3c57fe005d88/rocky-iv-w154.jpg\",\"id\":\"4bc9101e017a3c57fe005d88\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":759,\"width\":570,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-original.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":666,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-mid.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":246,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-cover.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":122,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-thumb.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":455,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-w342.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":205,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/d92/4bc9101f017a3c57fe005d92/rocky-iv-w154.png\",\"id\":\"4bc9101f017a3c57fe005d92\"}},{\"image\":{\"type\":\"poster\",\"size\":\"original\",\"height\":1450,\"width\":1018,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-original.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"mid\",\"height\":712,\"width\":500,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-mid.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"cover\",\"height\":263,\"width\":185,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-cover.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"thumb\",\"height\":131,\"width\":92,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-thumb.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w342\",\"height\":487,\"width\":342,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-w342.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}},{\"image\":{\"type\":\"poster\",\"size\":\"w154\",\"height\":219,\"width\":154,\"url\":\"http://cf1.imgobject.com/posters/16e/4c7eb2895e73d648e100016e/rocky-iv-w154.jpg\",\"id\":\"4c7eb2895e73d648e100016e\"}}],\"backdrops\":[{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":1080,\"width\":1920,\"url\":\"http://cf1.imgobject.com/backdrops/0a7/4d4ab4a17b9aa13ab40010a7/rocky-iv-original.jpg\",\"id\":\"4d4ab4a17b9aa13ab40010a7\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/0a7/4d4ab4a17b9aa13ab40010a7/rocky-iv-poster.jpg\",\"id\":\"4d4ab4a17b9aa13ab40010a7\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/0a7/4d4ab4a17b9aa13ab40010a7/rocky-iv-thumb.jpg\",\"id\":\"4d4ab4a17b9aa13ab40010a7\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/0a7/4d4ab4a17b9aa13ab40010a7/rocky-iv-w1280.jpg\",\"id\":\"4d4ab4a17b9aa13ab40010a7\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d7f/4bc9101d017a3c57fe005d7f/rocky-iv-original.jpg\",\"id\":\"4bc9101d017a3c57fe005d7f\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/d7f/4bc9101d017a3c57fe005d7f/rocky-iv-poster.jpg\",\"id\":\"4bc9101d017a3c57fe005d7f\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/d7f/4bc9101d017a3c57fe005d7f/rocky-iv-thumb.jpg\",\"id\":\"4bc9101d017a3c57fe005d7f\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d7f/4bc9101d017a3c57fe005d7f/rocky-iv-w1280.jpg\",\"id\":\"4bc9101d017a3c57fe005d7f\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d7b/4bc9101b017a3c57fe005d7b/rocky-iv-original.jpg\",\"id\":\"4bc9101b017a3c57fe005d7b\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/d7b/4bc9101b017a3c57fe005d7b/rocky-iv-poster.jpg\",\"id\":\"4bc9101b017a3c57fe005d7b\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/d7b/4bc9101b017a3c57fe005d7b/rocky-iv-thumb.jpg\",\"id\":\"4bc9101b017a3c57fe005d7b\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d7b/4bc9101b017a3c57fe005d7b/rocky-iv-w1280.jpg\",\"id\":\"4bc9101b017a3c57fe005d7b\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d77/4bc9101b017a3c57fe005d77/rocky-iv-original.jpg\",\"id\":\"4bc9101b017a3c57fe005d77\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/d77/4bc9101b017a3c57fe005d77/rocky-iv-poster.jpg\",\"id\":\"4bc9101b017a3c57fe005d77\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/d77/4bc9101b017a3c57fe005d77/rocky-iv-thumb.jpg\",\"id\":\"4bc9101b017a3c57fe005d77\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d77/4bc9101b017a3c57fe005d77/rocky-iv-w1280.jpg\",\"id\":\"4bc9101b017a3c57fe005d77\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"original\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d73/4bc9101b017a3c57fe005d73/rocky-iv-original.jpg\",\"id\":\"4bc9101b017a3c57fe005d73\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"poster\",\"height\":439,\"width\":780,\"url\":\"http://cf1.imgobject.com/backdrops/d73/4bc9101b017a3c57fe005d73/rocky-iv-poster.jpg\",\"id\":\"4bc9101b017a3c57fe005d73\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"thumb\",\"height\":169,\"width\":300,\"url\":\"http://cf1.imgobject.com/backdrops/d73/4bc9101b017a3c57fe005d73/rocky-iv-thumb.jpg\",\"id\":\"4bc9101b017a3c57fe005d73\"}},{\"image\":{\"type\":\"backdrop\",\"size\":\"w1280\",\"height\":720,\"width\":1280,\"url\":\"http://cf1.imgobject.com/backdrops/d73/4bc9101b017a3c57fe005d73/rocky-iv-w1280.jpg\",\"id\":\"4bc9101b017a3c57fe005d73\"}}],\"cast\":[{\"name\":\"Sylvester Stallone\",\"job\":\"Director\",\"department\":\"Directing\",\"character\":\"\",\"id\":16483,\"order\":0,\"cast_id\":1,\"url\":\"http://www.themoviedb.org/person/16483\",\"profile\":\"http://cf1.imgobject.com/profiles/316/4dceb7b57b9aa165ab001316/sylvester-stallone-thumb.jpg\"},{\"name\":\"Sylvester Stallone\",\"job\":\"Screenplay\",\"department\":\"Writing\",\"character\":\"\",\"id\":16483,\"order\":0,\"cast_id\":2,\"url\":\"http://www.themoviedb.org/person/16483\",\"profile\":\"http://cf1.imgobject.com/profiles/316/4dceb7b57b9aa165ab001316/sylvester-stallone-thumb.jpg\"},{\"name\":\"Robert Chartoff\",\"job\":\"Producer\",\"department\":\"Production\",\"character\":\"\",\"id\":16514,\"order\":0,\"cast_id\":3,\"url\":\"http://www.themoviedb.org/person/16514\",\"profile\":\"\"},{\"name\":\"Irwin Winkler\",\"job\":\"Producer\",\"department\":\"Production\",\"character\":\"\",\"id\":11472,\"order\":0,\"cast_id\":4,\"url\":\"http://www.themoviedb.org/person/11472\",\"profile\":\"\"},{\"name\":\"Vince DiCola\",\"job\":\"Original Music Composer\",\"department\":\"Sound\",\"character\":\"\",\"id\":16637,\"order\":0,\"cast_id\":5,\"url\":\"http://www.themoviedb.org/person/16637\",\"profile\":\"\"},{\"name\":\"Bill Butler\",\"job\":\"Director of Photography\",\"department\":\"Camera\",\"character\":\"\",\"id\":2287,\"order\":0,\"cast_id\":6,\"url\":\"http://www.themoviedb.org/person/2287\",\"profile\":\"\"},{\"name\":\"John W. Wheeler\",\"job\":\"Editor\",\"department\":\"Editing\",\"character\":\"\",\"id\":2512,\"order\":0,\"cast_id\":7,\"url\":\"http://www.themoviedb.org/person/2512\",\"profile\":\"\"},{\"name\":\"Don Zimmerman\",\"job\":\"Editor\",\"department\":\"Editing\",\"character\":\"\",\"id\":15005,\"order\":0,\"cast_id\":8,\"url\":\"http://www.themoviedb.org/person/15005\",\"profile\":\"\"},{\"name\":\"Bill Kenney\",\"job\":\"Production Design\",\"department\":\"Art\",\"character\":\"\",\"id\":10187,\"order\":0,\"cast_id\":9,\"url\":\"http://www.themoviedb.org/person/10187\",\"profile\":\"\"},{\"name\":\"Tom Bronson\",\"job\":\"Costume Design\",\"department\":\"Costume & Make-Up\",\"character\":\"\",\"id\":798,\"order\":0,\"cast_id\":10,\"url\":\"http://www.themoviedb.org/person/798\",\"profile\":\"\"},{\"name\":\"Stephen Abrums\",\"job\":\"Makeup Artist\",\"department\":\"Costume & Make-Up\",\"character\":\"\",\"id\":13436,\"order\":0,\"cast_id\":11,\"url\":\"http://www.themoviedb.org/person/13436\",\"profile\":\"\"},{\"name\":\"Leonard Engelman\",\"job\":\"Makeup Artist\",\"department\":\"Costume & Make-Up\",\"character\":\"\",\"id\":8870,\"order\":0,\"cast_id\":12,\"url\":\"http://www.themoviedb.org/person/8870\",\"profile\":\"\"},{\"name\":\"JoAnn May-Pavey\",\"job\":\"Unit Production Manager\",\"department\":\"Production\",\"character\":\"\",\"id\":16638,\"order\":0,\"cast_id\":13,\"url\":\"http://www.themoviedb.org/person/16638\",\"profile\":\"\"},{\"name\":\"David B. Cohn\",\"job\":\"Sound Editor\",\"department\":\"Sound\",\"character\":\"\",\"id\":16639,\"order\":0,\"cast_id\":14,\"url\":\"http://www.themoviedb.org/person/16639\",\"profile\":\"\"},{\"name\":\"James D. Young\",\"job\":\"Music Editor\",\"department\":\"Sound\",\"character\":\"\",\"id\":16640,\"order\":0,\"cast_id\":15,\"url\":\"http://www.themoviedb.org/person/16640\",\"profile\":\"\"},{\"name\":\"Howard Jensen\",\"job\":\"Special Effects\",\"department\":\"Crew\",\"character\":\"\",\"id\":16641,\"order\":0,\"cast_id\":16,\"url\":\"http://www.themoviedb.org/person/16641\",\"profile\":\"\"},{\"name\":\"Ron Lambert\",\"job\":\"Visual Effects\",\"department\":\"Visual Effects\",\"character\":\"\",\"id\":16642,\"order\":0,\"cast_id\":17,\"url\":\"http://www.themoviedb.org/person/16642\",\"profile\":\"\"},{\"name\":\"Gene LeBell\",\"job\":\"Stunts\",\"department\":\"Crew\",\"character\":\"\",\"id\":16643,\"order\":0,\"cast_id\":18,\"url\":\"http://www.themoviedb.org/person/16643\",\"profile\":\"\"},{\"name\":\"Sylvester Stallone\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Rocky Balboa\",\"id\":16483,\"order\":0,\"cast_id\":19,\"url\":\"http://www.themoviedb.org/person/16483\",\"profile\":\"http://cf1.imgobject.com/profiles/316/4dceb7b57b9aa165ab001316/sylvester-stallone-thumb.jpg\"},{\"name\":\"Talia Shire\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Adrian\",\"id\":3094,\"order\":1,\"cast_id\":20,\"url\":\"http://www.themoviedb.org/person/3094\",\"profile\":\"http://cf1.imgobject.com/profiles/695/4c9b772e7b9aa122df000695/talia-shire-thumb.jpg\"},{\"name\":\"Burt Young\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Paulie\",\"id\":4521,\"order\":2,\"cast_id\":21,\"url\":\"http://www.themoviedb.org/person/4521\",\"profile\":\"http://cf1.imgobject.com/profiles/1ec/4c225abc7b9aa135fd0001ec/burt-young-thumb.jpg\"},{\"name\":\"Carl Weathers\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Apollo Creed\",\"id\":1101,\"order\":3,\"cast_id\":22,\"url\":\"http://www.themoviedb.org/person/1101\",\"profile\":\"http://cf1.imgobject.com/profiles/136/4da131e55e73d66af5001136/carl-weathers-thumb.jpg\"},{\"name\":\"Brigitte Nielsen\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Ludmilla Vobet Drago\",\"id\":921,\"order\":4,\"cast_id\":23,\"url\":\"http://www.themoviedb.org/person/921\",\"profile\":\"http://cf1.imgobject.com/profiles/6a8/4cb935e85e73d677830006a8/brigitte-nielsen-thumb.jpg\"},{\"name\":\"Tony Burton\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Duke\",\"id\":16504,\"order\":5,\"cast_id\":24,\"url\":\"http://www.themoviedb.org/person/16504\",\"profile\":\"\"},{\"name\":\"Michael Pataki\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Nicoli Koloff\",\"id\":15993,\"order\":6,\"cast_id\":25,\"url\":\"http://www.themoviedb.org/person/15993\",\"profile\":\"\"},{\"name\":\"Dolph Lundgren\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Captain Ivan Drago\",\"id\":16644,\"order\":7,\"cast_id\":26,\"url\":\"http://www.themoviedb.org/person/16644\",\"profile\":\"http://cf1.imgobject.com/profiles/a40/4c5df2787b9aa151f4000a40/dolph-lundgren-thumb.jpg\"},{\"name\":\"Stu Nahan\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Commentator #1\",\"id\":16543,\"order\":8,\"cast_id\":27,\"url\":\"http://www.themoviedb.org/person/16543\",\"profile\":\"\"},{\"name\":\"R.J. Adams\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Sports Announcer\",\"id\":16645,\"order\":9,\"cast_id\":28,\"url\":\"http://www.themoviedb.org/person/16645\",\"profile\":\"\"},{\"name\":\"Al Bandiero\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"American Commentator #2\",\"id\":16646,\"order\":10,\"cast_id\":29,\"url\":\"http://www.themoviedb.org/person/16646\",\"profile\":\"\"},{\"name\":\"Dominic Barto\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Russian Government Official\",\"id\":16647,\"order\":11,\"cast_id\":30,\"url\":\"http://www.themoviedb.org/person/16647\",\"profile\":\"\"},{\"name\":\"Danial Brown\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Rocky Jr.'s Friend\",\"id\":16648,\"order\":12,\"cast_id\":31,\"url\":\"http://www.themoviedb.org/person/16648\",\"profile\":\"\"},{\"name\":\"James Brown\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"The Godfather of Soul\",\"id\":7172,\"order\":13,\"cast_id\":32,\"url\":\"http://www.themoviedb.org/person/7172\",\"profile\":\"http://cf1.imgobject.com/profiles/6fc/4c9b76bd7b9aa122de0006fc/james-brown-thumb.jpg\"},{\"name\":\"Rose Mary Campos\",\"job\":\"Actor\",\"department\":\"Actors\",\"character\":\"Maid\",\"id\":16649,\"order\":14,\"cast_id\":33,\"url\":\"http://www.themoviedb.org/person/16649\",\"profile\":\"\"}],\"version\":456,\"last_modified_at\":\"2011-10-09 12:27:14\"}]"));

			//if (id < 1)
			//    throw new ArgumentOutOfRangeException("id");

			//var url = "http://api.themoviedb.org/2.1/Movie.getInfo/en/json/" + _apiKey + "/" + id;

			//var request = WebRequest.Create(url);
			//var response = request.GetResponse();
			//if (response == null)
			//    throw new InvalidOperationException("The response for request \"" + url + "\" is null.");

			//var stream = response.GetResponseStream();
			//if (stream == null)
			//    throw new InvalidOperationException("The stream for the response for the request at \"" + url + "\" is null.");

			//using (var reader = new StreamReader(stream))
			//{
			//    var responseText = reader.ReadToEnd();

			//    var deserializer = new JavaScriptSerializer();
			//    return AdaptDetails(deserializer.Deserialize<dynamic>(responseText));
			//}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Adapts a response to a useable collection of remote movie objects.
		/// </summary>
		/// <param name="response">The repsonse from the remote server.</param>
		/// <returns>The adapted information.</returns>
		private static IEnumerable<RemoteMovie> AdaptSearchResults(dynamic response)
		{
			var movies = new List<RemoteMovie>();

			if (response[0] is string)
				return movies;

			foreach (var movie in response)
			{
				movies.Add(new RemoteMovie
				{
					ID = Convert.ToInt32(movie["id"]),
					Title = movie["name"],
					PosterLocation = DerivePoster(movie)
				});
			}
			return movies;
		}

		/// <summary>
		/// Derives the location of a poster from the response.
		/// </summary>
		/// <param name="movie">The search request's response.</param>
		/// <returns>The location of a poster.</returns>
		private static string DerivePoster(dynamic movie)
		{
			dynamic poster = null;
			foreach (var image in movie["posters"])
			{
				var width = Convert.ToInt32(image["image"]["width"]);
				var height = Convert.ToInt32(image["image"]["height"]);
				if (poster == null || width * height > poster.width * poster.height)
					poster = new { width, height, url = image["image"]["url"] };
			}

			return poster == null ? null : poster.url;
		}

		/// <summary>
		/// Adapts retrieved detailed movie information into a useable movie object.
		/// </summary>
		/// <param name="info">The retrieved information.</param>
		/// <returns>The adapted information.</returns>
		private Movie AdaptDetails(dynamic info)
		{
			var data = info[0];
			var year = new DateTime(Convert.ToInt32(data["released"].Substring(0, 4)), 1, 1);
			var movie = new Movie
			{
				DateAdded = DateTime.Now,
				Description = data["overview"],
				Name = data["name"],
				PosterLocation = DerivePoster(data),
				Year = year
			};

			var cast = data["cast"];
			var actors = new List<Person>();
			var producers = new List<Person>();
			foreach (IDictionary<string, object> member in cast)
			{
				var person = _personRepository.GetByName(member["name"] as string);
				if (person == null)
				{
					person = new Person {FirstName = ((string) member["name"]).Split(' ')[0], LastName = ((string) member["name"]).Split(' ')[1]};
					_personRepository.SaveOrUpdate(person);
				}

				switch ((string)member["job"])
				{
					case "Director":
						movie.Director = person;
						break;
					case "Producer":
						producers.Add(person);
						break;
					case "Actor":
						actors.Add(person);
						break;
				}
			}

			movie.Producers = producers;
			movie.Actors = actors;

			return movie;
		}

		/// <summary>
		/// Sets the cast for a movie.
		/// </summary>
		/// <param name="movie">The movie.</param>
		/// <param name="cast">The cast.</param>
		private void SetCast(Movie movie, IDictionary<string, dynamic> cast)
		{
			
		}
		#endregion
	}
}