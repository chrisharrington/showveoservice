if (!Showveo.Home)
	Showveo.Home = {};
if (!Showveo.Home.UncategorizedMovies)
	Showveo.Home.UncategorizedMovies = {};

Showveo.Home.UncategorizedMovies.Search = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The width of movies displayed in the search results panel.
	var _movieWidth;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* movieWidth: The width of movies displayed in the search results panel.
	*/
	this.initialize = function (parameters) {
		_movieWidth = parameters.movieWidth;

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

		$.ajax({
			type: "GET",
			url: "remotemovies/search",
			data: { query: _components.textSearch.val() },
			success: populate,
			error: Showveo.Controls.Feedback.error
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

		_components.results = new Showveo.Home.MovieGrid({
			panel: panel.find(">div.r"),
			movieWidth: _movieWidth
		});
	};

	/*
	* Populates search results into the search results table.
	* movies: The result of a search query.
	*/
	var populate = function (movies) {
		_components.results.load(movies);
	};

	this.initialize(parameters);
};