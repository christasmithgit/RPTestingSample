Feature: RP Startup
	In order to ensure that the service is hosted correctly,
	As an engineer,
	I want to host my service in a test host and validate it

Background:
	Given I am running the KX Resource Provider in a test server

Scenario Outline: Get to the default WeaterForecast controller returns 200 (OK) when user is authorized
	When I 'Get' the API '/WeatherForecast'
	Then I receive HTTP status code '200'
	And the response contains a JSON Object
	And the response is an array containing '5' items
