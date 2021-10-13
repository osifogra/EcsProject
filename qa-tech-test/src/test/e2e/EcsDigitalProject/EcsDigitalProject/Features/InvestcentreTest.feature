@UiTest
Feature: Arrays Challenge

	# Below are a number of rows with integers
    # Your job is to use selenium to read the dom and create an array data structure for each of the rows.

	# Once you have each array, write a function that is able to return the index of the array 
	# where the sum of integers at the index on the left is equal to the sum of integers on the right.
	# If there is no index return null

Scenario: Arrays sum test 
 Given I navigate to site
  When I select the challenge option
   And I choose to select table row '1'
   And create array one with selected table row data up to index '4' then miss the next index value and then create array two with the remaining data
   And I choose to submit challenge '1' arrays where the sum of integers at the index on the left is equal to the sum of integers on the right
   And I choose to select table row '2'
   And create array one with selected table row data up to index '3' then miss the next index value and then create array two with the remaining data
   And I choose to submit challenge '2' arrays where the sum of integers at the index on the left is equal to the sum of integers on the right
   And I choose to select table row '3'
   And create array one with selected table row data up to index '5' then miss the next index value and then create array two with the remaining data
   And I choose to submit challenge '3' arrays where the sum of integers at the index on the left is equal to the sum of integers on the right
   And I choose to enter my name as 'Graham Osifo' and submit the challenge 
   Then I should see my challenge is submitted