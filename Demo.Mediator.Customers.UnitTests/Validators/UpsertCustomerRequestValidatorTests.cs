using System;
using AutoFixture;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Validators;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Mediator.Customers.UnitTests
{
    public class UpsertCustomerRequestValidatorTests
    {
        private readonly Fixture _fixture;
        private UpsertCustomerRequest _request;
        private UpsertCustomerRequestValidator _validator;
        private ValidationResult _validationResult;
        
        public UpsertCustomerRequestValidatorTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(null, null, null)]
        public void AllFieldsAreRequiredWhenCreating(string name, string userName, string address)
        {
            this.Given(x => GivenFieldsAreSet(name, userName, address))
                .And(x => GivenIdIsNotProvided())
                .When(x => WhenValidationIsPerformed())
                .Then(x => ThenValidationMustFail(ErrorCodes.AllAreRequired, ErrorMessages.AllAreRequired))
                .BDDfy();
        }
        
        [Theory]
        [InlineData("name", "", "")]
        [InlineData("", "", "")]
        [InlineData(null, "username", null)]
        public void AnyOfTheFieldsIsRequiredWhenUpdating(string name, string userName, string address)
        {
            this.Given(x => GivenFieldsAreSet(name, userName, address))
                .And(x => GivenIdIsProvided())
                .When(x => WhenValidationIsPerformed())
                .Then(x => ThenValidationMustPass())
                .BDDfy();
        }

        private void GivenIdIsProvided()
        {
            _request.Id = Guid.NewGuid().ToString("N");
        }

        private void ThenValidationMustPass()
        {
            _validationResult.IsValid.Should().BeTrue();
        }

        private void ThenValidationMustFail(string errorCode, string errorMessage)
        {
            _validationResult.Errors.Should().ContainSingle(failure => failure.ErrorCode == errorCode && failure.ErrorMessage == errorMessage);
        }

        private void WhenValidationIsPerformed()
        {
            _validationResult = _validator.Validate(_request);
        }

        private void GivenIdIsNotProvided()
        {
            _request.Id = null;
        }

        private void GivenFieldsAreSet(string name, string userName, string address)
        {
            _validator = new UpsertCustomerRequestValidator();
            
            _request = _fixture.Create<UpsertCustomerRequest>();
            _request.Name = name;
            _request.UserName = userName;
            _request.Address = address;
        }
    }
}