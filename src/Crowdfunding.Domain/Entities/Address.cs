using System;
using System.Collections.Generic;
using Crowdfunding.Domain.Base;
using FluentValidation;

namespace Crowdfunding.Domain.Entities
{
    public class Address : Entity
    {
        public Address()
        {
        }

        public Address(
            Guid id,
            string cEP, 
            string textAddress, 
            string complement, 
            string number, 
            string city, 
            string state, 
            string phone
        )
        {
            this.Id = id;
            this.CEP = cEP;
            this.TextAddress = textAddress;
            this.Complement = complement;
            this.Number = number;
            this.City = city;
            this.State = state;
            this.Phone = phone;
        }

        public string CEP { get; set; }
        public string TextAddress { get; set; }
        public string Complement { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public String Phone { get; set; }

        public ICollection<Donation> Donation { get; set; }
        public override bool Valid()
        {
            ValidationResult = new ValidationAddress().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ValidationAddress : AbstractValidator<Address>
    {
        private const int MAX_LENGTH_ADDRESS = 250;
        private const int MAX_LENGTH_COMPLEMENT = 250;
        private const int MAX_LENGTH_CITY = 250;
        private const int MAX_LENGTH_NUMBER = 250;

        public ValidationAddress()
        {
            RuleFor(cep => cep.CEP)
                .NotEmpty()
                .WithMessage("The CEP field must be filled.")
                .Must(ValidCep)
                .WithMessage("The CEP field invalid.");
            
            RuleFor(textAddress => textAddress.TextAddress)
                .NotEmpty()
                .WithMessage("The address field must be filled.")
                .MaximumLength(MAX_LENGTH_ADDRESS)
                .WithMessage($"The address field must have at most {MAX_LENGTH_ADDRESS} character");

            RuleFor(number => number.Number)
                .NotEmpty()
                .WithMessage("The number field must be filled.")
                .MaximumLength(MAX_LENGTH_NUMBER)
                .WithMessage($"The number field must have at most {MAX_LENGTH_NUMBER} character");
            
            RuleFor(city => city.City)
                .NotEmpty()
                .WithMessage("The city field must be filled.")
                .MaximumLength(MAX_LENGTH_CITY)
                .WithMessage($"The city field must have at most {MAX_LENGTH_CITY} character");
            
            RuleFor(state => state.State)
                .MaximumLength(2)
                .WithMessage("The state field invalid");
            
            RuleFor(phone => phone.Phone)
                .NotEmpty()
                .WithMessage("The phone field must be filled.")
                .Must(ValidPhone)
                .WithMessage("Invalid phone field.");

            RuleFor(complement => complement.Complement)
                .MaximumLength(MAX_LENGTH_COMPLEMENT)
                .WithMessage($"The city field must have at most {MAX_LENGTH_COMPLEMENT} character");
        }
        private bool ValidCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return true;

            return cep.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Length == 8;
        }
        private bool ValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return true;

            var sizePhone = phone
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty).Length;
            return sizePhone >= 10 && sizePhone <= 11;
        }
    }
}