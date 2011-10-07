if (!Showveo.Controls)
	Showveo.Controls = { };

/*
* A class used to periodically check to see if any encoding tasks are in progress and, if so, displays them
* to the user.
*/
Showveo.Controls.EncodingChecker = function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The common components for the control.
	var _components;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	*/
	this.initialize = function (parameters) {
		loadComponents(parameters.panel);

		checkContinuous();
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

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
		_components.panelProgress = panel.find(">div>div");
		_components.labelFile = panel.find(">span>b");
		_components.labelPercentage = panel.find(">b");
	};

	/*
	* Continuously checks for new encoding tasks. If tasks exist, displays them to the user.
	*/
	var checkContinuous = function () {
		$.ajax({
			type: "GET",
			url: "movies/encoding",
			success: function (tasks) {
				//populate(tasks);
				setTimeout(function () {
					checkContinuous();
				}, 5000);
			}
		});
	};

	/*
	* Populates the encoding task area with the given task information.
	* tasks: The current encoding tasks.
	*/
	var populate = function (tasks) {
		if (tasks.length == 0) {
			_components.panel.fadeOut(200);
			return;
		}

		var task = tasks[0];
		if (task.File.length > 20)
			task.File = task.File.substring(0, 17) + "...";
		_components.labelFile.text(task.File);
		_components.panelProgress.css("width", task.PercentComplete + "%");
		_components.labelPercentage.text(Math.round(task.PercentComplete * 100) / 100);
		_components.panel.fadeIn(200);
	};

	this.initialize(parameters);
};