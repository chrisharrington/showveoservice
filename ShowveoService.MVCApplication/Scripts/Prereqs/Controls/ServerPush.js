if (!Showveo.Controls)
	Showveo.Controls = { };

/*
* A control used to provide a listener for server initiated communication.
*/
Showveo.Controls.ServerPush = new function (parameters) {

	//-------------------------------------------------------------------------------------
	/* Data Members */

	// The client used to listen for server messages.
	var _client;

	//-------------------------------------------------------------------------------------
	/* Constructors */

	/*
	* The default constructor.
	*/
	this.initialize = function () {
		_client = fm.websync.client;
	};

	//-------------------------------------------------------------------------------------
	/* Public Methods */

	/*
	* Begins listening on a channel for incoming messages.
	* @channel The name of the channel to listen to.
	* @callback The callback method to execute with incoming message information.
	*/
	this.listen = function (channel, callback) {
		_client.initialize();
		_client.connect({
			onSuccess: function () { onConnectionEstablished(callback); },
			onFailure: function (args) { Showveo.Controls.Feedback.error("An error has occurred while connecting to the encoding update stream."); }
		});
	};

	//-------------------------------------------------------------------------------------
	/* Event Handlers */

	/*
	* Fired after a successful connection has been established with the server.
	* @callback The callback method to fire with incoming server message information.
	*/
	var onConnectionEstablished = function (callback) {
		_client.subscribe({
			channel: "/encoding",
			onSuccess: function () { },
			onFailure: function () { Showveo.Controls.Feedback.error("An error occurred while subscribing to the encoding update stream."); },
			onReceive: function (args) {
				if (args.data)
					callback(args.data);
			}
		});

	};

	//-------------------------------------------------------------------------------------
	/* Private Methods */

};