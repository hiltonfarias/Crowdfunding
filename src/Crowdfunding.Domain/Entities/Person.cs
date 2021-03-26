using FluentValidation;

using System;
using System.Collections.Generic;

using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class Person : Entity
    {
        private string _name;
        public string Email { get; private set; }
        public bool Anonymous { get; private set; }
        public string SupportMessage { get; private set; }
        public string Name { 
            get { return Anonymous ? Email : _name; } 
            private set { _name = value; }
        }

        public ICollection<Donation> Donation { get; set; }

        public Person()
        {
        }

        public Person(Guid id, string name, string email, bool anonymous, string supportMessage)
        {
            Id = id;
            _name = name;
            Email = email;
            Anonymous = anonymous;
            SupportMessage = supportMessage;
        }

        public override bool Valid()
        {
            ValidationResult = new ValidationPerson().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ValidationPerson : AbstractValidator<Person>
    {
        private const int MAX_LENGTH_FIELD = 150;
        private const int MAX_LENGTH_MESSAGE = 500;
        public ValidationPerson()
        {
            RuleFor(name => name.Name)
                .NotEmpty()
                .WithMessage("The name field is required.")
                .When(anonymous => anonymous.Anonymous == false)
                .MaximumLength(MAX_LENGTH_FIELD)
                .WithMessage($"The Name field must have a maximum of {MAX_LENGTH_FIELD} characters.");
            
            RuleFor(email => email.Email)
                .NotEmpty()
                .WithMessage("The email field is required.")
                .MaximumLength(MAX_LENGTH_FIELD)
                .WithMessage($"THe email field must have a maximum of {MAX_LENGTH_FIELD} characters.");
            
            RuleFor(email => email.Email)
                .EmailAddress()
                .When(email => !string.IsNullOrEmpty(email.Email))
                .When(email => email?.Email?.Length <= MAX_LENGTH_FIELD)
                .WithMessage("The email field is invalid.");
            
            RuleFor(supportMessage => supportMessage.SupportMessage)
                .MaximumLength(MAX_LENGTH_MESSAGE)
                .WithMessage($"The support message field must have a maximum of {MAX_LENGTH_MESSAGE} characters.");
        }
    }
}