if (!Showveo.Home)
	Showveo.Home = {};

/*
* A control used to display a collection of movies in a grid.
*/
Showveo.Home.MovieGrid = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The number of movies to show on a row.
	var _columns;

	// The callback function to fire after the user has selected a movie.
	var _onMovieSelected;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* columns: The number of columns in each row of movies.
	* onMovieSelected: The callback function to fire after the user has selected a movie.
	*/
	this.initialize = function (parameters) {
		_columns = parameters.columns;
		_onMovieSelected = parameters.onMovieSelected;

		loadComponents(parameters.panel);
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Loads the movie grid with a collection of movies.
	* movies: The movies to load into the grid.
	* callback: The callback function to execute after all movies have been loaded.
	*/
	this.load = function (movies, callback) {
		_components.panel.find(">div:not(.clear)").remove();
		_components.labelEmpty.hide();

		if (!movies || movies.length == 0) {
			_components.labelEmpty.show();
			if (callback)
				callback();
		}
		else
			populateMovies(movies, callback);
	};

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

	/*
	* Shows the movies in the movie grid by fading each movie in one at a time.
	*/
	this.fade = function () {
		_components.panel.show().find("img").hide();
		_components.panel.find("img").each(function () {
			var poster = $(this);
			setTimeout(function () {
				poster.fadeIn(250);
			}, Math.random() * 1000);
		});
	};

	/*
	* Shows the movie grid by sliding it down.
	*/
	this.slide = function () {
		_components.panel.slideDown(200);
	};

	/*
	* Clears any movies from the grid.
	*/
	this.clear = function () {
		_components.panel.hide().find(">div").remove();
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.labelEmpty = panel.find(">span.e");
	};

	/*
	* Populates a movie panel with a given list of movies.
	* movies: The list of movies used to populate the movies panel.
	* callback: The callback function to execute after all movies have been loaded.
	*/
	var populateMovies = function (movies, callback) {
		var isVisible = _components.panel.is(":visible");
		var panelWidth = _components.panel.show().width();
		if (!isVisible)
			_components.panel.hide();
		
		if ($(window).height() <= $(document).height())
			panelWidth -= 10;
		
		var width = Math.floor((panelWidth / _columns) - 12);
		var count = 0;
		var loaded = 0;
		while (count < movies.length) {
			var row = $("<div></div>");
			var min = Math.min(movies.length - count, _columns);
			for (var i = 0; i < min; i++) {
				row.append(Showveo.Home.MovieCreator.create(movies[count], width, _onMovieSelected));
				count++;
			}
			row.insertBefore(_components.panel.find("div.clear"));
		}

		_components.panel.find("img").load(function () {
			if (++loaded == movies.length && callback)
				callback();
		});
	};

	this.initialize(parameters);
};