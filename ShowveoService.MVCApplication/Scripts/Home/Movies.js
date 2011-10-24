/*
* A control used to display a collection of movies.
*/
Showveo.Home.Movies = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//	The number of movies to display in each row.
	var _columns;

	//	The currently shown grid.
	var _grid;

	//	The callback function to execute after a movie has been categorized.
	var _onMovieCategorized;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* onMovieCategorized: The callback function to execute after a movie has been categorized.
	*/
	this.initialize = function (parameters) {
		_onMovieCategorized = parameters.onMovieCategorized;
		_columns = 10;

		loadComponents(parameters.panel);
		//loadMovies();
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user has selected a movie.
	* movie: The selected movie.
	*/
	var onMovieSelected = function (movie) {

	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Shows the appropriate movie grid control.
	* grid: The name of the movie grid to show.
	*/
	this.select = function (grid) {
		if (_grid) {
			_grid.hide(function () {
				if (_components[grid])
					_components[grid].show();
			});
		}
		else if (_components[grid])
			_components[grid].show();

		_grid = _components[grid];
	};

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;

		_components.all = new Showveo.Home.MovieGrid({
			panel: panel.find("div.all"),
			columns: _columns,
			onMovieSelected: onMovieSelected
		});

		_components.latest = new Showveo.Home.MovieGrid({
			panel: panel.find("div.latest"),
			columns: _columns,
			onMovieSelected: onMovieSelected
		});

		_components.uncategorized = new Showveo.Home.UncategorizedMovies.UncategorizedMovies({
			panel: panel.find("div.uncategorized"),
			columns: _columns,
			onMovieCategorized: function () {
				loadMovies();
				_components.uncategorized.hide(function () {
					_components.latest.show();
				});

				Showveo.LocationManager.navigate("latest");

				if (_onMovieCategorized)
					_onMovieCategorized();
			}
		});

		_components.grids = new Array(_components.all, _components.latest, _components.uncategorized);
	};

	/*
	* Loads the collection of all movies from the server.
	* callback: The callback method to execute after the movies have been retrieved.
	*/
	var loadMovies = function () {
		$.ajax({
			type: "GET",
			url: "movies/all",
			success: function (usermovies) { _components.all.load(extractMovies(usermovies)); },
			error: Showveo.Controls.Feedback.error
		});

		$.ajax({
			type: "GET",
			url: "movies/latest",
			success: function (usermovies) { _components.latest.load(extractMovies(usermovies)); },
			error: Showveo.Controls.Feedback.error
		});
	};

	/*
	* Extracts a movie collection from a collection of user-movie objects.
	* @usermovies The user-movies collection.
	* @return The extracted movies collection.
	*/
	var extractMovies = function (usermovies) {
		var movies = new Array();
		$(usermovies).each(function (index, usermovie) {
			movies.push(usermovie.Movie);
		});
		return movies;
	};

	this.initialize(parameters);
};