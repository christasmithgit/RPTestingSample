Feature: Storage service 

Background:
	Given I have an instance of the storage service

Scenario: Check the storage service successfully adds each string to the list
	Given I add 'helloWorld' to the list 
	Then the string 'helloWorld' should be in the list
