using NetCore_Mediator_CleanArch.Domain.Validation;

namespace NetCore_Mediator_CleanArch.Domain.Entities;

public sealed class Category : Entity
{
    public ICollection<Product>? Products { get; set; }

    public Category(string? name) => ValidateDomain(name);

    public Category(int id, string name)
    {
        DomainValidationException.When(id < 0, "Invalid Id value");

        Id = id;
        ValidateDomain(name);
    }

    public void Update(string? name) => ValidateDomain(name);

    private void ValidateDomain(string? name)
    {
        DomainValidationException.When(name is null, "Invalid name. Name is required");
        DomainValidationException.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainValidationException.When(name?.Length < 3, "Invalid name, too short, minimum 3 characters");

        Name = name;
    }
}
