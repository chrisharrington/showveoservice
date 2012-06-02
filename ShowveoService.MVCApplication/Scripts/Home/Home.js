Showveo.Home.Home = new function () {

	//--------------------------------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* The default constructor.
	*/
	this.initialize = function () {
		loadComponents();

		Showveo.LocationManager.navigate("all");
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	*/
	var loadComponents = function () {
		_components = {};
		_components.panel = $("div.c");

		_components.header = new Showveo.Controls.Header({
			panel: $("body>div.h")
		});

		_components.movies = new Showveo.Home.Movies({
			panel: _components.panel.find("div.m"),
			onUncategorizedMoviesLoaded: function (movies) {
				_components.header.setUncategorizedMovieCount(movies.length);
			}
		});

		Showveo.LocationManager.initialize({
			header: _components.header,
			movies: _components.movies
		});
	};

};