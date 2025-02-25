Feature: History

A short summary of the feature

@History
Scenario Outline:verify super user can check appointment history from history page
	Given user is at "History" page 
	And user verify the "<Date>",  "<facility>", "<health>" and "<comment>" 
	Examples: 
	| Date       | facility                     | health | comment |
	| 12/12/2020 | Tokyo CURA Healthcare Center | Medicare |         |