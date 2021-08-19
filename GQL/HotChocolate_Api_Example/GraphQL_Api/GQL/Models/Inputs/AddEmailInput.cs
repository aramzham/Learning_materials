namespace GraphQL_Api.GQL.Models.Inputs
{
    public record AddEmailInput(
        string DomainWord,
        string DomainName,
        string DomainSuffix,
        string FreeEmail,
        string EmailAddress,
        string PersonId
        );
}