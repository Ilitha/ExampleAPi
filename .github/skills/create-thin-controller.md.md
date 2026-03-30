# .github/skills/create-thin-controller.md

# Skill: Create Thin Controller

## Purpose

Use this skill when creating a new API controller or adding a new endpoint to an existing controller.

This skill is intentionally narrow.
It is only about generating controller code that follows repository patterns.
It is not responsible for reviewing the whole architecture.
It is not responsible for defining business rules.
It is not responsible for deciding final security policy.

## Inputs

Provide:

* the feature or endpoint description
* the service that should own the use case
* the expected request DTO
* the expected response DTO
* any known authorization requirement if already decided

## Instructions

Generate controller code that follows the repository’s existing controller pattern.

### Required behavior

* keep the controller thin
* use constructor injection
* call the appropriate service
* use DTOs at the API boundary
* follow existing route and naming conventions
* match the repository’s existing style

### Do not

* put business logic in the controller
* access DbContext directly
* call repositories directly unless the repository explicitly uses that pattern
* invent a new controller style
* expose entities directly
* create security rules that were not provided

### Authorization handling

If authorization requirements are explicitly provided, use them.

If authorization requirements are not explicitly provided:

* do not invent roles or policies
* if the repository normally uses authorization on this kind of endpoint, add a clear placeholder comment such as:
  `// TODO: Apply correct authorization policy if required by this endpoint`

### DTO handling

* use existing DTOs when appropriate
* only create a new DTO if needed
* do not use persistence entities as public request/response contracts

### Output

Return only:

1. controller code
2. any new request/response DTOs if absolutely necessary
3. short notes listing any TODO items requiring human validation

## Success Criteria

The generated controller should:

* be small and easy to read
* delegate business work to a service
* look consistent with the rest of the repo
* require minimal cleanup for architecture compliance

## Example Use

Create a controller endpoint for retrieving a 5-day weather forecast by latitude and longitude using IWeatherService.
