Feature: VerifyJouneyPlanner
	Verify the jouney planner functionality using the TFL website

Scenario: Verify valid journey can be planned using the widget
	Given I open the URL using a chosen browser
	Given I have accepted cookies
	Then I can check Plan a journey section is present
	When I enter "Stratford" into the From Journey Field
	And I enter "North Wembley" into the To Journey Field
	And I click on the Plan my journey Button
	Then I should see the Journey results
	And I should close the browser

Scenario: Verify the widget is unable to plan a journey when no locations are entered
	Given I open the URL using a chosen browser
	Given I have accepted cookies
	Then I can check Plan a journey section is present
	When I enter " " into the From Journey Field
	And I enter " " into the To Journey Field
	And I click on the Plan my journey Button
	Then I should see the error message displayed against the fields	
	And I should close the browser

Scenario: Verify the widget is unable to provide results when invalid journey is planned
	Given I open the URL using a chosen browser
	Given I have accepted cookies
	Then I can check Plan a journey section is present
	When I enter "@$%$%£%$" into the From Journey Field
	And I enter "£$£$%$%" into the To Journey Field
	And I click on the Plan my journey Button
	Then I should see the field validation error message displayed on the screen
	And I should close the browser

Scenario: Verify that the jouney can be amended using the Edit Jouney button
    Given I open the URL using a chosen browser
	Given I have accepted cookies
	Then I can check Plan a journey section is present
	When I enter "Luton" into the From Journey Field
	And I enter "Stevenage" into the To Journey Field
	And I click on the Plan my journey Button
	Then I should see the Journey results
	When I click the Edit Jouney Button
	Then I should see the edit journey page
	When I amend the jouney details
	And I click on the Update jouney button
	Then I should see the amended Journey results
	Then I should close the browser

Scenario: Verify change time link on the journey planneer displays "Arriving" option and plan a journey based on arrival time
Given I open the URL using a chosen browser
	Given I have accepted cookies
	Then I can check Plan a journey section is present
	When I enter "Stratford" into the From Journey Field
	And I enter "North Wembley" into the To Journey Field
	When I click on the link leaving change time link
	Then I should see Arriving option for the plan a journey
	When I click on the Plan my journey Button
	Then I should see the Journey results	
	Then I should close the browser