/*
* A control used to display a collection of movies.
*/
Showveo.Home.Movies = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//	The width of each movie in a row.
	var _movieWidth;

	//	The currently shown grid.
	var _grid;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* movieWidth: The width of each poster. Used in calculating how many movies should go in a row.
	*/
	this.initialize = function (parameters) {
		_movieWidth = 160;

		loadComponents(parameters.panel);
		loadMovies();

		_components.uncategorized.show();
		_grid = _components.uncategorized;
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Shows the appropriate movie grid control.
	* grid: The name of the movie grid to show.
	*/
	this.show = function (grid) {
		if (_grid) {
			_grid.hide(function () {
				if (_components[grid])
					_components[grid].show();
			});
		}

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
			movieWidth: _movieWidth
		});

		_components.latest = new Showveo.Home.MovieGrid({
			panel: panel.find("div.latest"),
			movieWidth: _movieWidth
		});

		_components.uncategorized = new Showveo.Home.UncategorizedMovieGrid({
			panel: panel.find("div.uncategorized"),
			movieWidth: _movieWidth
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
			success: _components.all.load,
			error: Showveo.Controls.Feedback.error
		});

		$.ajax({
			type: "GET",
			url: "movies/latest",
			success: _components.latest.load,
			error: Showveo.Controls.Feedback.error
		});
	};

	this.initialize(parameters);
};