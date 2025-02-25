Feature: Appointment


Scenario: verify appointment can not be done without selecting date

	Given user is at "Appointment" page 
	When user does not enter date 
	Then appointment cannot be made



	
Scenario Outline:verify user can select different values from facility dropdown and can make appointments 

	Given user is at "Appointment" page 
    And user enter date  
	When user selects different values from "<parameter1>"
	Then appointment can be made
	Examples: 
	| parameter1|
	|Hongkong CURA Healthcare Center|
	|Tokyo CURA Healthcare Center|

