Showveo.Home.Home = new function (parameters) {

	//--------------------------------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* The default constructor.
	*/
	this.initialize = function (parameters) {
		loadComponents();
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user has selected a menu item.
	* menu: The menu item to show.
	*/
	var onMenuSelected = function (menu) {
		_components.movies.show(menu);
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
			panel: $("body>div.h"),
			onMenuItemSelected: onMenuSelected
		});

		_components.movies = new Showveo.Home.Movies({
			panel: _components.panel.find("div.m"),
			selectMenu: _components.header.select
		});
	};

};