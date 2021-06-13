using AutoFixture;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.Validators;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Mediator.Customers.UnitTests
{
    public class CreateCustomerCommandValidatorTests
    {
        private readonly Fixture _fixture;
        private CreateCustomerCommand _command;
        private ValidationResult _validationResult;
        private CreateCustomerCommandValidator _validator;

        public CreateCustomerCommandValidatorTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void NameIsRequired(string name)
        {
            this.Given(x => GivenNameIsNotProvided(name))
                .When(x => WhenValidationIsPerformed())
                .Then(x => ThenMustFailWithNameValidationError())
                .BDDfy();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void AddressIsRequired(string address)
        {
            this.Given(x => GivenAddressIsNotProvided(address))
                .When(x => WhenValidationIsPerformed())
                .Then(x => ThenMustFailWithAddressValidationError())
                .BDDfy();
        }

        private void ThenMustFailWithNameValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, _command).WithErrorCode(ErrorCodes.Required).WithErrorMessage(ErrorMessages.Required);
        }

        private void ThenMustFailWithAddressValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Address, _command).WithErrorCode(ErrorCodes.Required).WithErrorMessage(ErrorMessages.Required);
        }

        private void WhenValidationIsPerformed()
        {
            _validationResult = _validator.Validate(_command);
        }

        private void GivenNameIsNotProvided(string name)
        {
            _validator = new CreateCustomerCommandValidator();
            _command = _fixture.Create<CreateCustomerCommand>();
            _command.Name = name;
        }

        private void GivenAddressIsNotProvided(string address)
        {
            _validator = new CreateCustomerCommandValidator();
            _command = _fixture.Create<CreateCustomerCommand>();
            _command.Address = address;
        }
    }
}