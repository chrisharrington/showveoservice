Showveo.Controls.Header = function (parameters) {

	//--------------------------------------------------------------------------------------------------------------
	/* Data Members */

	//	The common components for the control.
	var _components;

	//--------------------------------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* onMenuItemSelected: The callback function to execute after the user has selected a menu item.
	*/
	this.initialize = function (parameters) {
		loadComponents(parameters.panel);
	};

	/*
	* Sets a panel as selected.
	* id: The ID of the panel to select.
	*/
	this.select = function (id) {
		_components.panel.find("span.s").removeClass("s");
		_components.panel.find("#" + id).addClass("s");
	};

	/*
	* Sets the uncategorized movie count.
	* @count The count.
	*/
	this.setUncategorizedMovieCount = function (count) {
		_components.uncategorized.setCount(count);
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after the user has clicked a menu item. Shows the appropriate panel.
	*/
	var onMenuItemClicked = function () {
		var menu = $(this);
		if (menu.hasClass("s"))
			return;

		Showveo.LocationManager.navigate(menu.attr("id"));
	};

	//--------------------------------------------------------------------------------------------------------------
	/* Private Methods */

	/*
	* Loads the common components for the control.
	* panel: The panel containing the control elements.
	*/
	var loadComponents = function (panel) {
		_components = {};
		_components.panel = panel;
		_components.textSearch = panel.find("div.s>input").clearbox("Search");
		_components.labelMenuItems = panel.find("div.m>span").click(onMenuItemClicked);

		_components.uncategorized = new Showveo.Controls.UncategorizedMovies({
			menu: panel.find("#uncategorized")
		});

		_components.encodingchecker = new Showveo.Controls.EncodingChecker({
			panel: panel.find("div.m>div.e"),
			onEncodingComplete: _components.uncategorized.incrementUncategorizedCount
		});
	};

	this.initialize(parameters);
};