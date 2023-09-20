
# User story:

As a user, I should be able to register an account with username and password, and sign in the api with a Bearer token.

A user should be able to create WorkItems tied to the user. A work item is an entity that contains a Title and a Description. Both fields are required and a Title should be between 1-100 characters.


# Layers:

## Tasks-API:



## Data:

The implementation of the generic repository uses EF Core to access the database.

## Domain:

Defines data entities, data models used by the api, services.
Defines a generic repository interface that is consumed by the services.
Services abstract the inner working of the funcionalities. They receive a call to perform and action, perform validation using FluentValidation, may throw custom exceptions in case of entity validation errors.
Services will enforce business rules, like only allowing an user to see or interact with its own data, as well as specific validation using FluentValidation.
The services use AutoMapper to map between data models and database entities.

# Potential improvements:

## Unit tests

I have created simple unit tests to demonstrate some ideas for tests. Not all use cases were tested simply to save time, as there would be no reason to do basic tests on all endpoints and this is a project that was done during my free time.

As far as unit testing the data layer, we could mock or create an overwrite for the DataContext, using an in memory data structure and ensure that items will be added removed and updated in the data structure in memory, to replace the database activity and be sure the data layer itself is what is being tested.

## Magic numbers

In some situations I have used magic numbers, like in the WorkItemValidator. The max length for the Title property is hardcoded as 100, this of course could be retrieved from the configuration or even from a database configuration table.
