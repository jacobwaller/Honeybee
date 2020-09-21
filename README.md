# Honeybee

Honeybee is a reference architecture for automated testing of APIs using .NET Core.

## Design

Honeybee is designed specifically for rapid automation. This allows for more time to be spent on having a wide variety of tests versus spending time on setup for every new test. Honeybee achieves this by using a base class for shared methods, as well as using FluentAssertions for human-readable assertions for a variety of scenarios.

## Performance

Honeybee sacrifices performance for easy automation and readable design. Because our API calls may modify a database, and may overwrite data, we typically run our tests serially rather than in parallel to allow the tests to be completely independent of one another. This is only a recommendation and it is possible to set tests up to be run in parallel, but it is recommended to reset any back-end data storage back to some base state before every test that writes to that data. This data reset can be done in the constructor of the base class as that is run before each individual test.

## Setup

1.  Clone the repository
2.  Open the solution in Visual Studio (optional if you know what you're doing)
3.  Build the project
4.  Done.

It's that easy. From there, you **should** probably modify the tests, base class, and models to fit something closer to your needs.

## Dependencies & Reasoning

### xUnit

The unit testing framework xUnit is used because of its flexibility with Theory's and its built in test isolation with one object per test method. This means that fields are re-initialized after every test, which aids in test isolation.

### Fluent Assertions

Fluent Assertions was chosen because of its easy readability and extensive built in functionality. It allows one line assertions to verify complex situations in a much more understandable way than the built in assertions library.

### RestSharp

RestSharp was chosen because of how easily it is able to handle authentication. It also has automatic serialization and easy extensibility to allow easy deserialization.

## Adapting Honeybee for your own project

### Honeybee.Test.Common

- `appsettings.local.json`
  - Store tokens, keys, etc., here that you don't want checked into source control
  - Should be in the root of Honeybee.Test.Common
- `Models/`
  - For every high level model, we have them named with the schema:
    `{Action}{Object}{Response/Request}Model`
    For example, the class that would be POST-ed to create a new Order would be called:
    `CreateOrderRequestModel`
- `BaseApi_Test.cs`
  - The project is currently configured to run all tests serially, remove `[Collection("Sequential")]` to run all tests in parallel. Or, if there are groups of tests which **can** be run parallel to your main API tests, you can create another base class that extends `BaseApi_Test.cs` with a different collection name.
  - This contains the setup and teardown methods. (Constructor and `Dispose()` methods respectively)
  - This is also where base API calls are defined. (`CreateUser()` and `GetUser()`)
    - We put the API calls in base class methods so that you can add/change authentication in one place as the project progresses.
- `Extensions.cs`
  - Define common extension methods here.
  - The only extension method currently defined is `DeserializeContent<T>()`.
    - `response.DeserializeContent<CreateUserResponseModel>()` is more readable than
      `JsonConvert.Deserialize<CreateUserResponseModel>(response.Content)`

### Honeybee.Test.Api

This is where the bulk of your code will go. Assuming the base class is configured correctly, preemptive automation should be fairly easy. There are a few samples in the project directory that highlight how easy it is to write new tests for your API. Remember, the goal of Honeybee is to be human-readable and quickly-writeable. This means that, not including initialization of models, tests can be just a few lines long and still test complex functionality.

** Some tests are currently failing. ** This is because regres does not handle errors in a way you might expect. I configured the sample tests to something more realistic, but expect failing tests if you run what we've got without modification.
