Feature: SpecFlowMediatekTests

Scenario: Log in
	Given the pseudo is 'Karen Administratif'
	And the password is 'pwdadministratif'
	When we press the submit button
	Then the user should be logged in