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

	// The callback function to execute once an encoding task completes.
	var _onEncodingComplete;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	* panel: The panel containing the control elements.
	* onEncodingComplete: The callback function to execute once an encoding task completes.
	*/
	this.initialize = function (parameters) {
		_onEncodingComplete = parameters.onEncodingComplete;

		loadComponents(parameters.panel);

		//checkContinuous();
		subscribeToChannel();
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
				populate(tasks);
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
		if (task.PercentComplete == 100) {
			_components.panel.fadeOut(200);
			_onEncodingComplete();
			return;
		}

		if (task.File.length > 20)
			task.File = task.File.substring(0, 17) + "...";
		_components.labelFile.text(task.File);
		_components.panelProgress.css("width", task.PercentComplete + "%");
		_components.labelPercentage.text(derivePercentString(task.PercentComplete));
		_components.panel.fadeIn(200);
	};

	/*
	* Derives a percent complete string from a percentage.
	* @percentage The percent complete.
	* @returns The derived string.
	*/
	var derivePercentString = function (percentage) {
		var string = (Math.round(percentage * 100) / 100).toString();
		if (string.indexOf(".") == -1)
			string += ".00";

		var parts = string.split(".");
		var first = parts[0];
		if (first.length == 1)
			string = "0" + string;

		var second = parts[1];
		if (second.length == 1)
			string += "0";

		return string + "%";
	};

	/*
	* Subscribes to the encoding update channel.
	*/
	var subscribeToChannel = function () {
		Showveo.Controls.ServerPush.listen("encoding", function (task) {
			inspect(task);
			if (task.PercentComplete == 100) {
				_components.panel.fadeOut(200);
				_onEncodingComplete();
				return;
			}

			if (task.File.length > 20)
				task.File = task.File.substring(0, 17) + "...";
			_components.labelFile.text(task.File);
			_components.panelProgress.css("width", task.PercentComplete + "%");
			_components.labelPercentage.text(derivePercentString(task.PercentComplete));
			_components.panel.fadeIn(200);
		});
	};

	this.initialize(parameters);
};