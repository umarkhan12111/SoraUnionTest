
Feature: Login

@Login
Scenario: Verify User cannot login with invalid credentails
	Given user Open login page
	And user enter password as "abc" and username as "John Doe"
	When the user click on login button
	Then login should be unsuccessfull

	
@Login
Scenario: Verify User can login with valid credentails
	Given user Open login page
	And user enter password as "ThisIsNotAPassword" and username as "John Doe"
	When the user click on login button
	Then login should be successfull



@Login	
Scenario: Verify User page UI
	Given user Open login page
	Then User see the Login page UI
