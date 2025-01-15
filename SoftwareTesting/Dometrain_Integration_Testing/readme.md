Unit testing tests a class, a method or a one line of code.

In unit testing we don't have any db calls, network calls or file system calls, they are all mocked.

As part of integration testing we have the db, file system, network etc.

Integration tests give you more realistic view of how your application works.

use "dotnet dev-certs https -ep cert.pfx -p Test1234!" to create a certificate

# The 5 integration testing steps
1. Setup - you might need to create a db, spin up a docker container, seed some data in db, some things before your tests run
2. Dependency mocking - not like in unit tests, f.e. you have a GitHub api dependency, then you might want to mock it to test the failure scenario, or maybe that api is rate limited and you don't want to waste it
3. Execution - with xUnit
4. Assertion - the data returned from execution is a correct one
5. Cleanup - clean after yourself

tests in 1 class will run one after the other

xUnit will instantiate test class once per execution

xUnit uses ctor as Setup
if your setup is async use IAsyncLifetime interface
if you combine ctor and IAsyncLifetime together ctor still will be used first ahead of InitializeAsync

xUnit uses IDisposable interface's Dispose method or IAsyncLifetime's DisposeAsync() methods for cleanup

use MemberData or ClassData if you have multiple input cases for the same test

[Fact(Skip = "don't run this test for now")] - skip the test with a reason

you'd need to add package Microsoft.AspNetCore.Mvc.Testing for WebApplicationFactory

use IClassFixture<WebApplicationFactory<your_api_assembly_marker>> to inject the application factory once for the test class instead of spinning up for each individual test

for realistic looking test data use Bogus with Faker<your_type>().RuleFor

in order not to leave any test data in your db, add the ids of added items into a list and in cleanup method (Dispose/DisposeAsync) delete them by id

if you have create, update, delete methods in your controller, it's a good idea to create a folder by your controller name (CustomerController) and for each action create a test class like CreateCustomerControllerTests, DeleteCustomerControllerTest etc.

if you want your webapplictionfactory to be one for the tests above you need to use ICollectionFixture<WebApplicationFactory<IApiMarker>> with [CollectionDefinition("CustomerApi Collection for example")] attribute and repeat the same attribute in the test classes

dockerfile defines how shall the image is built

if you have a dependency on a live api, f.e. GitHub api, you ultimately don't want to test it, or maybe you'll be rate limited, for that purpose use WireMock which will mock the live api and return the responses you expect

if you want your background services not interfere with your testing workflow you can remove them from DI container like this "services.RemoveAll(typeof(IHostedService))"

DO NOT ever change the database to an in-memory one!! you're killing the purpose of integration testing

for testing the database you can remove the dbContext type and yours with connection string in docker
