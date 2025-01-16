using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;

namespace Customers.Api.Tests.Integration.CustomerController;

public class UpdateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public UpdateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Update_UpdatesUser_WhenDataIsValid()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        var updateCustomer = _customerGenerator.Clone().Generate();

        // Act
        var result = await _client.PutAsJsonAsync($"customers/{customerResponse!.Id}", updateCustomer);

        // Assert
        var customerInDb = await _client.GetFromJsonAsync<CustomerResponse>($"customers/{customerResponse.Id}");
        customerInDb.Should().BeEquivalentTo(updateCustomer);
    }

    [Fact]
    public async Task Update_ReturnsValidationError_WhenEmailIsInvalid()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        var updateCustomer = _customerGenerator.Clone().RuleFor(x => x.Email, _ => "invalidEmail").Generate();

        // Act
        var action = () =>  _client.PutAsJsonAsync($"customers/{customerResponse!.Id}", updateCustomer);

        // Assert
        await action.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact]
    public async Task Update_ReturnsValidationError_WhenGitHubUserDoestNotExist()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createResponse = await _client.PostAsJsonAsync("customers", customer);
        var customerResponse = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();
        var updateCustomer = _customerGenerator.Clone().RuleFor(x => x.GitHubUsername, _ => "invalidGitHubUser").Generate();

        // Act
        var action = () =>  _client.PutAsJsonAsync($"customers/{customerResponse!.Id}", updateCustomer);

        // Assert
        await action.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}