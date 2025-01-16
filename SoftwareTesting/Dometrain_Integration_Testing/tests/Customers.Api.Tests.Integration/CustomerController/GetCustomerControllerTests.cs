using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;

namespace Customers.Api.Tests.Integration.CustomerController;

public class GetCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public GetCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        
        // Act
        var response = await _client.GetFromJsonAsync<CustomerResponse>($"customers/{customerResponse!.Id}");

        // Assert
        response.Should().NotBeNull();
        response.Should().BeEquivalentTo(customerResponse);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}