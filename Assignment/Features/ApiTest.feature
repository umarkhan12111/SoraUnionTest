Feature: ApiTest



@API
Scenario:Verify that the employee “Michael Silva” has a salary of 198500
	Given Admin access the "api/v1/employees"
	Then verify "Michael Silva" has a salary of "198500"


