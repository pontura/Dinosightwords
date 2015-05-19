
// Use Parse.Cloud.define to define as many cloud functions as you want.
// For example:
Parse.Cloud.define("hello", function(request, response) {
  response.success("Hello world!" + request.params.user);
});

Parse.Cloud.define('sendDiploma', function(request, response) {

	var Mandrill = require('mandrill');
	Mandrill.initialize('EMwZ78XKVJNat5f4OE1XmQ');

	Mandrill.sendEmail({
	  message: {
		html: "<img src='" + request.params.url + "' />",
		subject: request.params.username + " Won a new Diploma in Sight Words",
		from_email: "bot@tipitap.com",
		from_name: "Tipitap",
		to: [
		  {
			email: request.params.to,
			name: request.params.username
		  }
		]
	  },
	  async: true
	},{
	  success: function(httpResponse) {
		console.log(httpResponse);
		response.success("Email sent!");
	  },
	  error: function(httpResponse) {
		console.error(httpResponse);
		response.error("Uh oh, something went wrong");
	  }
	});


});


