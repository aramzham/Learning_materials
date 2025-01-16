using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;

namespace Customers.Api.Tests.Integration.CustomerController;

public class GetAllCustomersControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public GetAllCustomersControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }
    
    [Fact]
    public async Task GetAll_ReturnsAllCustomers_WhenCustomersExist()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        
        // Act
        var response = await _client.GetAsync("customers");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var content = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
        content?.Customers.Should().NotBeNull();
        content!.Customers.Should().HaveCount(1);
        
        // cleanup
        await _client.DeleteAsync($"customers/{customerResponse!.Id}");
    }
    
    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoCustomersExist()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("customers");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var content = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
        content?.Customers.Should().BeEmpty();
    }
}