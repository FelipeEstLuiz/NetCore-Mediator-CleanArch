using FluentAssertions;
using NetCore6_Mediator_CleanArch.Domain.Entities;

namespace NetCore6_Mediator_CleanArch.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionValidation()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should().Throw<Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionValidationShortName()
        {
            Action action = () => new Category(1, "Ca");
            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionValidationRequiredName()
        {
            Action action = () => new Category(1, "");
            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionValidationRequiredName()
        {
            Action action = () => new Category(1, null);
            action
                .Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }
    }
}
