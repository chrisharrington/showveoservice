if (!Showveo.Home)
	Showveo.Home = {};
if (!Showveo.Home.UncategorizedMovies)
	Showveo.Home.UncategorizedMovies = {};

Showveo.Home.UncategorizedMovies.Search = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The number of movies in each row of search results.
	var _columns;

	// The callback function to execute once the user has selected a movie.
	var _onMovieSelected;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* columns: The number of movies in each row of search results.
	* onMovieSelected: The callback function to execute once the user has selected a movie.
	*/
	this.initialize = function (parameters) {
		_columns = parameters.columns;
		_onMovieSelected = parameters.onMovieSelected;

		loadComponents(parameters.panel);
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Shows this movie grid.
	* callback: The optional callback method fired after the grid has been shown.
	*/
	this.show = function (callback) {
		_components.panel.fadeIn(200, function () {
			if (callback)
				callback();
		});
	};

	/*
	* Hides this movie grid.
	* callback: The optional callback method fired after the grid has been hidden.
	*/
	this.hide = function (callback) {
		_components.panel.fadeOut(200, function () {
			if (callback)
				callback();
		});
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user has clicked the search button. Validates the user's input then
	* searches for movies related to the given query.
	*/
	var onSearchClicked = function () {
		if (_components.textSearch.val() == "") {
			Showveo.Controls.Feedback.error("The search query is required.");
			return;
		}

		Showveo.Controls.Feedback.clear();
		_components.results.clear();
		_components.imgLoader.show();
		_components.textSearch.attr("disabled", true);
		_components.buttonSearch.attr("disabled", true);

		$.ajax({
			type: "GET",
			url: "remotemovies/search",
			data: { query: _components.textSearch.val() },
			success: populate,
			error: function (error) {
				Showveo.Controls.Feedback.error(error);
				_components.imgLoader.hide();
				_components.textSearch.attr("disabled", false);
				_components.buttonSearch.attr("disabled", false);
			}
		});
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
		_components.textSearch = panel.find("input[type='text']");
		_components.buttonSearch = panel.find("button").click(onSearchClicked);
		_components.imgLoader = panel.find(">div.l");

		_components.results = new Showveo.Home.MovieGrid({
			panel: panel.find(">div.r"),
			columns: _columns,
			onMovieSelected: _onMovieSelected
		});
	};

	/*
	* Populates search results into the search results table.
	* movies: The result of a search query.
	*/
	var populate = function (movies) {
		_components.results.load(movies, function () {
			_components.imgLoader.hide();
			_components.textSearch.attr("disabled", false);
			_components.buttonSearch.attr("disabled", false);
			_components.results.fade();
		});
	};

	this.initialize(parameters);
};