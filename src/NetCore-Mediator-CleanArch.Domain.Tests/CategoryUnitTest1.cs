using FluentAssertions;
using NetCore_Mediator_CleanArch.Domain.Entities;

namespace NetCore_Mediator_CleanArch.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should().NotThrow<Validation.DomainValidationException>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionValidation()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should().Throw<Validation.DomainValidationException>().WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionValidationShortName()
    {
        Action action = () => new Category(1, "Ca");
        action
            .Should()
            .Throw<Validation.DomainValidationException>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionValidationRequiredName()
    {
        Action action = () => new Category(1, "");
        action
            .Should()
            .Throw<Validation.DomainValidationException>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExceptionValidationRequiredName()
    {
        Action action = () => new Category(1, name: string.Empty);
        action
            .Should()
            .Throw<Validation.DomainValidationException>()
            .WithMessage("Invalid name. Name is required");
    }
}
