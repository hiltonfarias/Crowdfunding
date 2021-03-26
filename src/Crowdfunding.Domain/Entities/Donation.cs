using System;
using Crowdfunding.Domain.Base;
using FluentValidation;

namespace Crowdfunding.Domain.Entities
{
    public class Donation : Entity
    {
        public double Value { get; private set; }
        public Guid PersonalDataId { get; private set; }
        public Guid BillingAddressId { get; private set; }
        public DateTime DateTime { get; private set; }
        public Person PersonalData { get; private set; }
        public Address BillingAddress { get; private set; }
        public CreditCard FormPayment { get; private set; }
        private Donation()
        {
        }

        public Donation(
            Guid id,
            double value, 
            Guid personalDataId, 
            Guid billingAddressId, 
            DateTime dateTime, 
            Person personalData, 
            Address billingAddress, 
            CreditCard formPayment
        )
        {
            this.Id = id;
            this.Value = value;
            this.PersonalDataId = personalDataId;
            this.BillingAddressId = billingAddressId;
            this.DateTime = dateTime;
            this.PersonalData = personalData;
            this.BillingAddress = billingAddress;
            this.FormPayment = formPayment;
        }

        public void UpdatePurchaseDate()
        {
            DateTime = DateTime.Now;
        }

        public void AddPerson(Person person)
        {
            PersonalData = person;
        }

        public void AddBillingAddress(Address address)
        {
            BillingAddress = address;
        }

        public void AddFormPayment(CreditCard formPayment)
        {
            FormPayment = formPayment;
        }
        public override bool Valid()
        {
            ValidationResult = new ValidationDonation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class ValidationDonation : AbstractValidator<Donation>
    {
        public ValidationDonation()
        {
            RuleFor(value => value.Value)
                .GreaterThanOrEqualTo(5)
                .WithMessage("Minimum donation amount is R$ 5,00.")
                .LessThanOrEqualTo(4500)
                .WithMessage("Maximum donation amount is R$ 4.500,00.");
            
            RuleFor(personaldata => personaldata.PersonalData)
                .NotNull()
                .WithMessage("The personal data is required.")
                .SetValidator(new ValidationPerson());
            
            RuleFor(billingAddress => billingAddress.BillingAddress)
                .NotNull()
                .WithMessage("The billing address data is required.")
                .SetValidator(new ValidationAddress());
            
            RuleFor(formPayment => formPayment.FormPayment)
                .NotNull()
                .WithMessage("The form payment data is required.")
                .SetValidator(new ValidationCreditCard());
        }
    }
}