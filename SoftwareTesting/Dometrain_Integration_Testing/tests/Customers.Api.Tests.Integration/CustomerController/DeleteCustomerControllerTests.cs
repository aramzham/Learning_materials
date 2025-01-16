using System.Net;
using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;

namespace Customers.Api.Tests.Integration.CustomerController;

public class DeleteCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public DeleteCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenCustomerExists()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        
        // Act
        var result = await _client.DeleteAsync($"customers/{customerResponse!.Id}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange

        // Act
        var result = await _client.DeleteAsync($"customers/{Guid.NewGuid()}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}