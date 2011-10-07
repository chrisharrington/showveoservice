if (!Showveo.Home)
	Showveo.Home = { };

/*
* A class used to control the display and selection of uncategorized movies.
*/
Showveo.Home.UncategorizedMovieGrid = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	// The width of the movies displayed in the search results panel.
	var _movieWidth;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* movieWidth: The width of the movies displayed in the search results panel.
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

	//-------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.panelList = panel.find(">div.l");
		_components.textSearch = panel.find(">div.s>input").clearbox("What's the name of this movie? Enter the year as well if you need to be specific.");

		_components.selectionGrid = new Showveo.Home.MovieGrid({
			panel: panel.find(">div.s"),
			movieWidth: _movieWidth
		});
	};

	this.initialize(parameters);
};