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

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	*/
	this.initialize = function (parameters) {
		_columns = 10;

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
			columns: _columns
		});

		_components.latest = new Showveo.Home.MovieGrid({
			panel: panel.find("div.latest"),
			columns: _columns
		});

		_components.uncategorized = new Showveo.Home.UncategorizedMovies.UncategorizedMovies({
			panel: panel.find("div.uncategorized"),
			columns: _columns
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
			success: function(movies) { _components.all.load(movies); },
			error: Showveo.Controls.Feedback.error
		});

		$.ajax({
			type: "GET",
			url: "movies/latest",
			success: function (movies) { _components.latest.load(movies); },
			error: Showveo.Controls.Feedback.error
		});
	};

	this.initialize(parameters);
};