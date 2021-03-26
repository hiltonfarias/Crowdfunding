using System;
using System.Globalization;
using Crowdfunding.Domain.Base;
using FluentValidation;

namespace Crowdfunding.Domain.Entities
{
    public class CreditCard : Entity
    {
        public string CardholderName { get; set; }
        public string CreditCardNumber { get; set; }
        public string Validity { get; set; }
        public string CVV { get; set; }
        public CreditCard()
        {
        }

        public CreditCard(string cardholderName, string creditCardNumber, string validity, string cVV)
        {
            CardholderName = cardholderName;
            CreditCardNumber = creditCardNumber;
            Validity = validity;
            CVV = cVV;
        }
        public override bool Valid()
        {
            ValidationResult = new ValidationCreditCard().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ValidationCreditCard : AbstractValidator<CreditCard>
    {
        private const int MAX_LENGTH_FIELD =150;
        public ValidationCreditCard()
        {
            RuleFor(cardHolderName => cardHolderName.CardholderName)
                .NotEmpty()
                .WithMessage("The card holder name field must be filled.")
                .MaximumLength(MAX_LENGTH_FIELD)
                .WithMessage($"The card holder name field must have at most {MAX_LENGTH_FIELD} characters.");
            
            RuleFor(creditCardNumber => creditCardNumber.CreditCardNumber)
                .NotEmpty()
                .WithMessage("The credit card number field must be filled.")
                .CreditCard()
                .WithMessage("The credit card number field is invalid.");
            
            RuleFor(cvv => cvv.CVV)
                .NotEmpty()
                .WithMessage("The CVV field must be filled.")
                .Must(ValidCVV)
                .WithMessage("The CVV field is invalid.");
            
            RuleFor(validity => validity.Validity)
                .NotEmpty()
                .WithMessage("The validity field must be filled.")
                .Must(valid => ValidateExpirationField(valid, out _))
                .WithMessage("Invalid credit card expiration date field")
                .Must(valid => ValidateExpirationDate(valid))
                .WithMessage("Credit Card with expired date");

        }

        private bool ValidateExpirationDate(string valid)
        {
            if (ValidateExpirationField(valid, out DateTime ? dueDate) && dueDate != null)
            {
                return DateTime.Now.Date <= ((DateTime)dueDate).Date;
            }
            return true;
        }

        private bool ValidateExpirationField(string valid, out DateTime ? date)
        {
            date = null;

            if (string.IsNullOrEmpty(valid)) return true;

            string[] monthYear = valid.Split("/");
            if (monthYear.Length == 2)
            {
                if (monthYear[0].Length <= 2 && monthYear[1].Length <= 4)
                {
                    int month, year;
                    if (int.TryParse(monthYear[0], out month) && int.TryParse(monthYear[1], out year))
                    {
                        date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ValidCVV(string cvv)
        {
            if (string.IsNullOrEmpty(cvv)) return true;

            return cvv.Length >= 3 && cvv.Length <= 4 && int.TryParse(cvv, out _);
        }
    }
}